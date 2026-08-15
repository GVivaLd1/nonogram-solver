namespace Core
{
    public class NonogramBoard
    {
        public string Name { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<int>> Rows { get; set; } = new();
        public List<List<int>> Columns { get; set; } = new();
    }
}
