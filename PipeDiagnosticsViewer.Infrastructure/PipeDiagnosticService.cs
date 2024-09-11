using PipeDiagnosticsViewer.Infrastructure.Import;
using PipeDiagnosticsViewer.Interfaces;
using PipeDiagnosticsViewer.Models;

namespace PipeDiagnosticsViewer.Infrastructure
{
    public class PipeDiagnosticFromFileService : IPipeDiagnosticFromFileService
    {
        private readonly string[] supportedExtensions = { "csv", "xls", "xlsx" };

        public string[] SupportedExtensions { get => supportedExtensions; }

        public IEnumerable<PipeDiagnostic> GetItems(string filePath)
        {
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using IMapper<PipeDiagnostic> mapper = GetMapper(stream, FileHelper.GetExtensionWihtoutPoint(filePath));
            return mapper.GetItems();
        }

        public async IAsyncEnumerable<PipeDiagnostic> GetItemsAsync(string filePath, CancellationToken cancellationToken)
        {
            await using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using IMapper<PipeDiagnostic> mapper = GetMapper(stream, FileHelper.GetExtensionWihtoutPoint(filePath));
            await foreach (PipeDiagnostic pipeDiagnostic in mapper.GetItemsAsync().WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return pipeDiagnostic;
        }

        private IMapper<PipeDiagnostic> GetMapper(Stream stream, string extension)
        {
            switch (extension)
            {
                case "csv": return new CSVMapper<PipeDiagnostic>(stream, new PipeDiagnosticsFactory());
                case "xls":
                case "xlsx": return new ExcelMapper<PipeDiagnostic>(stream, new PipeDiagnosticsFactory());
                default:  throw new ArgumentNullException($"Not supported extension: {extension}");
            }
        }
    }
}