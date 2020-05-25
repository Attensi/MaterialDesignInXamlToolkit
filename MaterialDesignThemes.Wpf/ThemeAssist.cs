using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal static class ThemeAssist
    {
        internal static Brush GetTriggerColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TriggerBrushProperty);
        }

        internal static void SetTriggerBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TriggerBrushProperty, value);
        }

        internal static readonly DependencyProperty TriggerBrushProperty =
            DependencyProperty.RegisterAttached("TriggerBrush", typeof(Brush), typeof(ThemeAssist), new PropertyMetadata(null));
    }
}
