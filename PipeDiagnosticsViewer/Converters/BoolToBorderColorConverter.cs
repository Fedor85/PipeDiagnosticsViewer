using System.Windows.Media;

namespace PipeDiagnosticsViewer.Converters
{
    public class BoolToBorderColorConverter : BaseBoolToColorConverter
    {
        public BoolToBorderColorConverter() : base(Colors.Black, Colors.DimGray)
        {
        }
    }
}