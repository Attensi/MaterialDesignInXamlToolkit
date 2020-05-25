using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class IsDarkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color? color = null;
            switch (value)
            {
                case SolidColorBrush brush:
                    color = brush.Color;
                    break;
                case Color c:
                    color = c;
                    break;
            }

            return color.HasValue
                ? Color.Equals(ContrastingForegroundColor(color.Value), Colors.White)
                : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static Color ContrastingForegroundColor(Color color)
        {
            double rgb_srgb(double d)
            {
                d = d / 255.0;
                return (d > 0.03928)
                    ? d = Math.Pow((d + 0.055) / 1.055, 2.4)
                    : d = d / 12.92;
            }
            var r = rgb_srgb(color.R);
            var g = rgb_srgb(color.G);
            var b = rgb_srgb(color.B);

            var luminance = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return luminance > 0.179 ? Colors.Black : Colors.White;
        }
    }
}