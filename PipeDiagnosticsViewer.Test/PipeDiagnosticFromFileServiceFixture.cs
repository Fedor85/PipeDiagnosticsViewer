using NUnit.Framework;
using PipeDiagnosticsViewer.Infrastructure;
using PipeDiagnosticsViewer.Models;
using NUnit.Framework.Legacy;

namespace PipeDiagnosticsViewer.Test
{
    [TestFixture]
    public class PipeDiagnosticFromFileServiceFixture
    {
        [Test]
        public void GetItemsFromCSVTest()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.objects.csv");
            IEnumerable<PipeDiagnostic> pipeDiagnostics = GetPipeDiagnostics(filePath);
            ClassicAssert.AreEqual(pipeDiagnostics.Count(), 9);
        }

        [Test]
        public async Task GetItemsFromCSVTestAsync()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.objects.csv");
            List<PipeDiagnostic> pipeDiagnostics = await GetPipeDiagnosticsA(filePath);
            ClassicAssert.AreEqual(pipeDiagnostics.Count(), 9);
        }

        [Test]
        public void GetItemsFromExcelTest()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.Objects.xlsx");
            IEnumerable<PipeDiagnostic> pipeDiagnostics = GetPipeDiagnostics(filePath);
            ClassicAssert.AreEqual(pipeDiagnostics.Count(), 4);
        }

        [Test]
        public async Task GetItemsFromExcelTTestAsync()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.Objects.xlsx");
            List<PipeDiagnostic> pipeDiagnostics = await GetPipeDiagnosticsA(filePath);
            ClassicAssert.AreEqual(pipeDiagnostics.Count, 4);
        }

        private IEnumerable<PipeDiagnostic> GetPipeDiagnostics(string filePath)
        {
            PipeDiagnosticFromFileService pipeDiagnosticFromFileService = new PipeDiagnosticFromFileService();
            IEnumerable<PipeDiagnostic> pipeDiagnostics = pipeDiagnosticFromFileService.GetItems(filePath);
            File.Delete(filePath);
            return pipeDiagnostics;
        }

        private async Task<List<PipeDiagnostic>> GetPipeDiagnosticsA(string filePath)
        {
            PipeDiagnosticFromFileService pipeDiagnosticFromFileService = new PipeDiagnosticFromFileService();
            List<PipeDiagnostic> pipeDiagnostics = new List<PipeDiagnostic>();
             await foreach (PipeDiagnostic pipeDiagnostic in pipeDiagnosticFromFileService.GetItemsAsync(filePath))
                pipeDiagnostics.Add(pipeDiagnostic);

            File.Delete(filePath);

            return pipeDiagnostics;
        }
    }
}
