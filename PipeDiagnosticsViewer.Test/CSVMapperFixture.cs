using NUnit.Framework;
using System.Reflection;
using PipeDiagnosticsViewer.Models;
using NUnit.Framework.Legacy;
using PipeDiagnosticsViewer.Infrastructure.Import;
using PipeDiagnosticsViewer.Infrastructure.Import.Base;
using PipeDiagnosticsViewer.Interfaces;

namespace PipeDiagnosticsViewer.Test
{
    [TestFixture]
    public class CSVMapperFixture
    {
        [Test]
        public void GetItemsTest()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PipeDiagnosticsViewer.Test.Data.objects.csv");
            using IMapper<PipeDiagnostic> csvMapper = new CSVMapper<PipeDiagnostic>(stream, new PipeDiagnosticsFactory());
            IEnumerable<PipeDiagnostic> pipeDiagnostics = csvMapper.GetItems();
            ClassicAssert.AreEqual(pipeDiagnostics.Count(), 9);
        }

        [Test]
        public async Task GetItemsAsyncTest()
        {
            await using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PipeDiagnosticsViewer.Test.Data.objects.csv");
            using IMapper<PipeDiagnostic> csvMapper = new CSVMapper<PipeDiagnostic>(stream, new PipeDiagnosticsFactory());
            List<PipeDiagnostic> pipeDiagnostics = new List<PipeDiagnostic>();
            await foreach (PipeDiagnostic pipeDiagnostic in csvMapper.GetItemsAsync())
                pipeDiagnostics.Add(pipeDiagnostic);

            ClassicAssert.AreEqual(pipeDiagnostics.Count, 9);
        }
    }
}
