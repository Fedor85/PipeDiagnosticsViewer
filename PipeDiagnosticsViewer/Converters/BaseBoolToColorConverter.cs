using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PipeDiagnosticsViewer.Converters
{
    public abstract class BaseBoolToColorConverter : IValueConverter
    {
        private Color colorTrue;

        private Color colorFalse;

        protected BaseBoolToColorConverter(Color colorTrue, Color colorFalse)
        {
            this.colorTrue = colorTrue;
            this.colorFalse = colorFalse;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(value is bool && (bool)value ? colorTrue: colorFalse);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is SolidColorBrush && ((SolidColorBrush)value).Color == colorTrue;
        }
    }
}