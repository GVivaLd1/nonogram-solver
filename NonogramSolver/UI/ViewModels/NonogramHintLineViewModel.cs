// NonogramHintLineViewModel.cs — новий файл
using System.Collections.ObjectModel;

namespace UI.ViewModels
{
    public class NonogramHintLineViewModel
    {
        public ObservableCollection<int> Hints { get; } = new();
    }
}