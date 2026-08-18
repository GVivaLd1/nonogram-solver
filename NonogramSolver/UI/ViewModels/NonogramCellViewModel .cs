using System;
using System.Collections.Generic;
using System.Text;

namespace UI.ViewModels
{
    public enum CellState
    {
        Empty,
        Filled,
        Crossed
    }

    public class NonogramCellViewModel : ViewModelBase
    {
        private CellState _state = CellState.Empty;
        public CellState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public int Row { get; init; }
        public int Column { get; init; }
    }
}
