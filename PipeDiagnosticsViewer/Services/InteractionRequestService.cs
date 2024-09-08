using System.Text;
using System.Windows;
using Microsoft.Win32;
using PipeDiagnosticsViewer.Interfaces;

namespace PipeDiagnosticsViewer.Services
{
    public class InteractionRequestService : IInteractionRequestService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(Application.Current.MainWindow, message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(Application.Current.MainWindow, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public string OpenFile(params string[] extensions)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = GetFilter(extensions);
            openFileDialog.ShowDialog(Application.Current.MainWindow);
            return openFileDialog.FileName;
        }

        private string GetFilter(params string[] extensions)
        {
            if (extensions.Length == 0)
                return String.Empty;

            StringBuilder filter = new StringBuilder();
            foreach (string extension in extensions)
                filter.AppendFormat("*.{0};", extension);

            return $"({filter})|{filter}";
        }
    }
}