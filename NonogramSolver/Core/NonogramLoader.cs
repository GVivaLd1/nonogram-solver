using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Core
{
    public class NonogramLoader
    {
        private static readonly JsonSerializerOptions Options = new ()
        {
            PropertyNameCaseInsensitive  = true
        };

        public static NonogramBoard Load(string path)
        {
            using var stream = File.OpenRead(path);
            var nonogram = JsonSerializer.Deserialize<NonogramBoard>(stream, Options) 
                ?? throw new InvalidDataException("Не вдалося розпарсити файл нонограми.");

            Validate(nonogram);
            return nonogram;
        }

        public static void Validate(NonogramBoard nonogram)
        {
            if (nonogram.Rows.Count == 0 || nonogram.Columns.Count == 0)
                throw new InvalidDataException("Файл не містить підказок для rows/columns");

            if (nonogram.Rows.Count != nonogram.Height) 
                throw new InvalidDataException("Кількість рядків не збігається з height");

            if (nonogram.Columns.Count != nonogram.Width)
                throw new InvalidDataException("Кількість рядків не збігається з width");
        }
    }
}
