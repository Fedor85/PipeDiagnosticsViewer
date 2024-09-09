using NUnit.Framework;
using PipeDiagnosticsViewer.Infrastructure;
using PipeDiagnosticsViewer.Models;
using NUnit.Framework.Legacy;
using PipeDiagnosticsViewer.Interfaces;

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
            List<PipeDiagnostic> pipeDiagnostics = await GetPipeDiagnosticsAsync(filePath, CancellationToken.None);
            ClassicAssert.AreEqual(pipeDiagnostics.Count, 9);
        }

        [Test]
        public void GetItemsFromExcelTest()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.Objects.xlsx");
            IEnumerable<PipeDiagnostic> pipeDiagnostics = GetPipeDiagnostics(filePath);
            ClassicAssert.AreEqual(pipeDiagnostics.Count(), 4);
        }

        [Test]
        public async Task GetItemsFromExcelTestAsync()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.Objects.xlsx");
            List<PipeDiagnostic> pipeDiagnostics = await GetPipeDiagnosticsAsync(filePath, CancellationToken.None);
            ClassicAssert.AreEqual(pipeDiagnostics.Count, 4);
        }

        [Test]
        public void MuitiItemsFromCSVTestAsync()
        {
            string filePath = TestHelper.GetTempFilePath("PipeDiagnosticsViewer.Test.Data.objects.csv");
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            List<PipeDiagnostic> pipeDiagnosticsBackup = new List<PipeDiagnostic>();
            List<PipeDiagnostic> pipeDiagnostics = new List<PipeDiagnostic>();
            Task task1 = AddPipeDiagnostic(pipeDiagnostics, filePath, cancelTokenSource.Token);
            cancelTokenSource.Cancel();
            pipeDiagnostics.Clear();
            pipeDiagnosticsBackup.AddRange(pipeDiagnostics);
            cancelTokenSource = new CancellationTokenSource();
            Task task2 = AddPipeDiagnostic(pipeDiagnostics, filePath, cancelTokenSource.Token);
            cancelTokenSource.Cancel();
            pipeDiagnostics.Clear();
            pipeDiagnosticsBackup.AddRange(pipeDiagnostics);
            cancelTokenSource = new CancellationTokenSource();
            Task task3 = AddPipeDiagnostic(pipeDiagnostics, filePath, cancelTokenSource.Token);
            cancelTokenSource.Cancel();
            pipeDiagnostics.Clear();
            pipeDiagnosticsBackup.AddRange(pipeDiagnostics);
            cancelTokenSource = new CancellationTokenSource();
            Task task4 = AddPipeDiagnostic(pipeDiagnostics, filePath, cancelTokenSource.Token);
            pipeDiagnosticsBackup.AddRange(pipeDiagnostics);
            Task.WaitAll(task1, task2, task3, task4);
            ClassicAssert.AreEqual(pipeDiagnosticsBackup.Count, 9);
        }

        private IEnumerable<PipeDiagnostic> GetPipeDiagnostics(string filePath)
        {
            IPipeDiagnosticFromFileService pipeDiagnosticFromFileService = new PipeDiagnosticFromFileService();
            IEnumerable<PipeDiagnostic> pipeDiagnostics = pipeDiagnosticFromFileService.GetItems(filePath);
            File.Delete(filePath);
            return pipeDiagnostics;
        }

        private async Task<List<PipeDiagnostic>> GetPipeDiagnosticsAsync(string filePath, CancellationToken cancellationToken, bool isDeleteFile = true)
        {
            IPipeDiagnosticFromFileService pipeDiagnosticFromFileService = new PipeDiagnosticFromFileService();
            List<PipeDiagnostic> pipeDiagnostics = new List<PipeDiagnostic>();
             await foreach (PipeDiagnostic pipeDiagnostic in pipeDiagnosticFromFileService.GetItemsAsync(filePath, cancellationToken))
                pipeDiagnostics.Add(pipeDiagnostic);
            
             if(isDeleteFile)
                File.Delete(filePath);

            return pipeDiagnostics;
        }

        private async Task AddPipeDiagnostic(List<PipeDiagnostic> pipeDiagnostics, string filePath, CancellationToken cancellationToken)
        {
            pipeDiagnostics.AddRange(await GetPipeDiagnosticsAsync(filePath, cancellationToken, false));
        }
    }
}
