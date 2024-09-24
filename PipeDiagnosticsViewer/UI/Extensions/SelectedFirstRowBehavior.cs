using Microsoft.Xaml.Behaviors;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;

namespace PipeDiagnosticsViewer.UI.Extensions
{
    public class SelectedFirstRowBehavior: Behavior<DataGrid>
    {
        private INotifyCollectionChanged? notifyCollectionChanged;
        
        private object? firstItem;

        protected override void OnAttached()
        {
            AssociatedObject.LoadingRow += LoadingRow;
            DependencyPropertyDescriptor? dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, AssociatedObject.GetType());
            dependencyPropertyDescriptor.AddValueChanged(AssociatedObject, ItemsSourceChanged);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.LoadingRow -= LoadingRow;
            DependencyPropertyDescriptor? dependencyPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, AssociatedType);
            dependencyPropertyDescriptor.RemoveValueChanged(AssociatedObject, ItemsSourceChanged);
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged -= ItemsSourceCollectionChanged;
        }

        private void ItemsSourceChanged(object? sender, EventArgs e)
        {
            notifyCollectionChanged = AssociatedObject.ItemsSource as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged += ItemsSourceCollectionChanged;
        }

        private void ItemsSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (firstItem != null)
                return;

            if (e.NewStartingIndex == 0 && e.NewItems?.Count > 0)
                firstItem = e.NewItems[0];
        }

        private void LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            if (firstItem == null)
                return;

            DataGridRow row = e.Row;
            if (!row.Item.Equals(firstItem))
                return;

            row.IsSelected = true;
            AssociatedObject.SelectedItem = firstItem;
            firstItem = null;
        }
    }
}