using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;

namespace PipeDiagnosticsViewer.UI.Chart
{
    /// <summary>
    /// Interaction logic for PipeDiagnosticsChart.xaml
    /// </summary>
    public partial class PipeDiagnosticsChart : UserControl
    {
        private static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(PipeDiagnosticsChart));

        private static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(PipeDiagnosticsChart), null, CheckValidation);
        
        private static readonly MethodInfo forceUpdateOverlaysMethod = typeof(ScatterSeries).GetMethod("UpdateDataPoint", BindingFlags.NonPublic | BindingFlags.Instance);
        
        private static readonly Size sizeEmpty = new(0, 0);
        
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public PipeDiagnosticsChart()
        {
            InitializeComponent();
            PipeDiagnosticsScatterSeries.SetBinding(ScatterSeries.ItemsSourceProperty, new Binding { Path = new PropertyPath(ItemsSourceProperty), Source = this });
            PipeDiagnosticsScatterSeries.SetBinding(ScatterSeries.SelectedItemProperty, new Binding { Path = new PropertyPath(SelectedItemProperty), Source = this });
        }

        private void DataPointSizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
            Size previousSize = e.PreviousSize;
            if (!previousSize.Equals(sizeEmpty))
                forceUpdateOverlaysMethod?.Invoke(PipeDiagnosticsScatterSeries,new object[] { sender as ScatterSelectDataPoint });
        }

        private static bool CheckValidation(object value)
        {
            return value == null || !value.ToString().Equals("{NewItemPlaceholder}");
        }
    }
}