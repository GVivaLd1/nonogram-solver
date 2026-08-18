using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System;
using Core;

namespace UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _loadedFileName = "";

    [ObservableProperty]
    private bool _isPuzzleLoaded = false;

    [ObservableProperty]
    private string _statusText = "";

    [ObservableProperty]
    private bool _isSolving = false;

    [ObservableProperty]
    private NonogramBoard? _currentBoard;

    // Сама сітка клітинок
    public ObservableCollection<NonogramRowViewModel> PuzzleRows { get; } = new();

    // Підказки зліва (по одній на кожен рядок)
    public ObservableCollection<NonogramHintLineViewModel> RowHints { get; } = new();

    // Підказки зверху (по одній на кожен стовпець)
    public ObservableCollection<NonogramHintLineViewModel> ColumnHints { get; } = new();

    [RelayCommand]
    private async Task LoadJsonAsync()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var storageProvider = desktop.MainWindow.StorageProvider;

            var result = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select a nonogram file",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("JSON files") { Patterns = new[] { "*.json" } },
                    new FilePickerFileType("all files") { Patterns = new[] { "*.*" } }
                }
            });

            if (result.Count >= 1)
            {
                var file = result[0];
                string filePath = file.Path.LocalPath;

                try
                {
                    var parsedBoard = NonogramLoader.Load(filePath);

                    CurrentBoard = parsedBoard;
                    BuildGridFromBoard(parsedBoard);

                    LoadedFileName = file.Name;
                    IsPuzzleLoaded = true;
                    StatusText = $"Готово! Дошка: {parsedBoard.Width}x{parsedBoard.Height}";
                }
                catch (Exception ex)
                {
                    StatusText = $"Помилка: {ex.Message}";
                    IsPuzzleLoaded = false;
                    LoadedFileName = "Помилка завантаження";
                }
            }
        }
    }

    private void BuildGridFromBoard(NonogramBoard board)
    {
        PuzzleRows.Clear();
        RowHints.Clear();
        ColumnHints.Clear();

        for (int r = 0; r < board.Height; r++)
        {
            var rowVm = new NonogramRowViewModel();
            for (int c = 0; c < board.Width; c++)
            {
                rowVm.Cells.Add(new NonogramCellViewModel { Row = r, Column = c });
            }
            PuzzleRows.Add(rowVm);

            var rowHintVm = new NonogramHintLineViewModel();
            foreach (var hint in board.Rows[r])
                rowHintVm.Hints.Add(hint);
            RowHints.Add(rowHintVm);
        }

        for (int c = 0; c < board.Width; c++)
        {
            var colHintVm = new NonogramHintLineViewModel();
            foreach (var hint in board.Columns[c])
                colHintVm.Hints.Add(hint);
            ColumnHints.Add(colHintVm);
        }
    }
}