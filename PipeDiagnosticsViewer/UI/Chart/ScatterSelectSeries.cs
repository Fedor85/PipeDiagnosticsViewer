using System.Reflection;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace PipeDiagnosticsViewer.UI.Chart
{
    [StyleTypedProperty(Property = DataPointStyleName, StyleTargetType = typeof(ScatterSelectDataPoint))]
    internal class ScatterSelectSeries : ScatterSeries
    {
        private MethodInfo getResourceDictionaryWithTargetTypeMethod;

        public ScatterSelectSeries()
        {
            getResourceDictionaryWithTargetTypeMethod = typeof(DataPointSeries).GetMethod("GetResourceDictionaryWithTargetType", BindingFlags.NonPublic | BindingFlags.Static);
        }

        protected override DataPoint CreateDataPoint()
        {
            return new ScatterSelectDataPoint();
        }

        protected override IEnumerator<ResourceDictionary> GetResourceDictionaryEnumeratorFromHost()
        {
            object? result = getResourceDictionaryWithTargetTypeMethod.Invoke(null, new object[] { SeriesHost, typeof(ScatterSelectDataPoint), true });
            return result as IEnumerator<ResourceDictionary>;
        }
    }
}