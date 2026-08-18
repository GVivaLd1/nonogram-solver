// Converters/CellStateToBrushConverter.cs
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using UI.ViewModels;

namespace UI.Converters
{
    public class CellStateToBrushConverter : IValueConverter
    {
        private static readonly IBrush FilledBrush = new SolidColorBrush(Color.Parse("#5B8CFF"));
        private static readonly IBrush EmptyBrush = new SolidColorBrush(Color.Parse("#20222E"));
        private static readonly IBrush CrossedBrush = new SolidColorBrush(Color.Parse("#2A2C3A"));

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is CellState state)
            {
                return state switch
                {
                    CellState.Filled => FilledBrush,
                    CellState.Crossed => CrossedBrush,
                    _ => EmptyBrush
                };
            }
            return EmptyBrush;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}