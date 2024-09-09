using System.Globalization;
using System.Windows.Data;

namespace PipeDiagnosticsViewer.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? "yes" : "no";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string && ((string)value).Equals("yes");
        }
    }
}