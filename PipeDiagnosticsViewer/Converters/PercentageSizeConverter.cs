using System.Globalization;
using System.Windows.Data;

namespace PipeDiagnosticsViewer.Converters
{
    internal class PercentageSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not IConvertible || values[1] is not IConvertible)
                return 0d;

            double maxSize = ((IConvertible)values[2]).ToDouble(null);
            if (maxSize.Equals(0))
                return 0d;

            double chartAtualSize = ((IConvertible)values[0]).ToDouble(null);
            double size = ((IConvertible)values[1]).ToDouble(null);
            return (chartAtualSize * size)/ maxSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}