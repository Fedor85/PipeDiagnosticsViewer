using System.Windows.Media;

namespace PipeDiagnosticsViewer.Converters
{
    internal class BoolToBackgroundColorConverter: BaseBoolToColorConverter
    {
        public BoolToBackgroundColorConverter(): base(Colors.Red, Colors.LawnGreen)
        {
        }
    }
}