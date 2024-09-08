using System.Globalization;
using System.Windows.Data;

namespace PipeDiagnosticsViewer.Converters
{
    internal class BorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? 4 : 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  value is IConvertible && (int)value == 4;
        }
    }
}