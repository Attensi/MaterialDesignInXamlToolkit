using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class GridLinesVisibilityBorderToThicknessConverter : IValueConverter
    {
        private const double GridLinesThickness = 1;
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DataGridGridLinesVisibility visibility))
                return Binding.DoNothing;

            var thickness = parameter as double? ?? GridLinesThickness;

            switch (visibility)
            {
                case DataGridGridLinesVisibility.All:
                    return new Thickness(0, 0, thickness, thickness);
                case DataGridGridLinesVisibility.Horizontal:
                    return new Thickness(0, 0, 0, thickness);
                case DataGridGridLinesVisibility.Vertical:
                    return new Thickness(0, 0, thickness, 0);
                case DataGridGridLinesVisibility.None:
                    return new Thickness(0);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}