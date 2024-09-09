using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PipeDiagnosticsViewer.UI.Extensions
{
    public static class DataGridKeyEnterExtension
    {
        public static readonly DependencyProperty RegisterProperty = DependencyProperty.RegisterAttached("Register", typeof(bool), typeof(DataGridKeyEnterExtension), new PropertyMetadata(RegisterKeyEnterEvent));

        public static bool GetRegister(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(RegisterProperty);
        }

        public static void SetRegister(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(RegisterProperty, value);
        }

        private static void RegisterKeyEnterEvent(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = dependencyObject as DataGrid;
            if (dataGrid == null)
                return;

            if ((bool)e.NewValue)
                dataGrid.PreviewKeyDown += PreviewKeyDown;
            else
                dataGrid.PreviewKeyDown -= PreviewKeyDown;
        }

        private static void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            e.Handled = true;
            ((DataGrid)sender).CommitEdit(DataGridEditingUnit.Row, true);
            KeyEventArgs tabKeyEvent = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Tab);
            tabKeyEvent.RoutedEvent = Keyboard.KeyDownEvent;
            InputManager.Current.ProcessInput(tabKeyEvent);
        }
    }
}