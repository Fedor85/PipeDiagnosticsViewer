using PipeDiagnosticsViewer.Models;

namespace PipeDiagnosticsViewer.Interfaces
{
    public interface IPipeDiagnosticFromFileService
    {
        string[] SupportedExtensions { get; }

        IEnumerable<PipeDiagnostic> GetItems(string filePath);

        IAsyncEnumerable<PipeDiagnostic> GetItemsAsync(string filePath);
    }
}