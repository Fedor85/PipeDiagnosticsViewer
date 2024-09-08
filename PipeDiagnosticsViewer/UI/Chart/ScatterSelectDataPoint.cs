using System.Reflection;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Input;

namespace PipeDiagnosticsViewer.UI.Chart
{
    internal class ScatterSelectDataPoint : ScatterDataPoint
    {
        private static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(ScatterSelectDataPoint));

        private static readonly EventInfo eventInfo;

        private static readonly Type eventHandlerType;

        private static readonly MethodInfo selectHandlerMethod;

        private bool isSelectHandler;

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        static ScatterSelectDataPoint()
        {
            eventInfo = typeof(ScatterSelectDataPoint).GetEvent("IsSelectedChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            eventHandlerType = eventInfo.EventHandlerType;
            Type? eventArgsType = eventHandlerType.GetMethod("Invoke").GetParameters()[1].ParameterType;
            MethodInfo genericSelectHandlerMethod = typeof(ScatterSelectDataPoint).GetMethod("SelectHandler", BindingFlags.Instance | BindingFlags.NonPublic);
            selectHandlerMethod = genericSelectHandlerMethod.MakeGenericMethod(eventArgsType);
        }

        public ScatterSelectDataPoint()
        {
            SubscribeToSelectEvent();
            MouseLeave += MouseOverHandler;
            MouseEnter += MouseOverHandler;
        }

        private void SubscribeToSelectEvent()
        {
            Delegate selectDelegat = Delegate.CreateDelegate(eventHandlerType, this, selectHandlerMethod);
            MethodInfo? addMethod = eventInfo.GetAddMethod(true);
            addMethod.Invoke(this, new[] { selectDelegat });
        }

        private void SelectHandler<T>(object? sender, T args)
        {
            RoutedPropertyChangedEventArgs<bool> changedEventArgs = args as RoutedPropertyChangedEventArgs<bool>;
            isSelectHandler = changedEventArgs.NewValue;
            UpdateSelected();
        }

        private void MouseOverHandler(object sender, MouseEventArgs e)
        {
            UpdateSelected();
        }

        private void UpdateSelected()
        {
            IsSelected = IsMouseOver || isSelectHandler;
        }
    }
}