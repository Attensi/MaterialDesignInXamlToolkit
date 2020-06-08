using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ExpanderDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ExpandDirection direction
                && targetType != null
                && parameter is string values)
            {
                var directionValues = values.Split(',');
                if (directionValues.Length == 4)
                {
                    int index;
                    switch (direction)
                    {
                        case ExpandDirection.Down:
                            index = 3;
                            break;
                        case ExpandDirection.Up:
                            index = 1;
                            break;
                        case ExpandDirection.Left:
                            index = 0;
                            break;
                        case ExpandDirection.Right:
                            index = 2;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    var converter = TypeDescriptor.GetConverter(targetType);

                    return converter.CanConvertFrom(typeof(string))
                        ? converter.ConvertFromInvariantString(directionValues[index])
                        : directionValues[index];
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
