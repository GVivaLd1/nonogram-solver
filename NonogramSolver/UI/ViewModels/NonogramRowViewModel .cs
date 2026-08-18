using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UI.ViewModels
{
    public class NonogramRowViewModel : ViewModelBase
    {
        public ObservableCollection<NonogramCellViewModel> Cells { get; } = new();
    }
}
