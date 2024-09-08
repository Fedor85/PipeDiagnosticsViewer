using NUnit.Framework;
using System.Reflection;
using PipeDiagnosticsViewer.Models;
using PipeDiagnosticsViewer.Infrastructure.Import;
using PipeDiagnosticsViewer.Infrastructure.Import.Base;
using PipeDiagnosticsViewer.Interfaces;
using NUnit.Framework.Legacy;

namespace PipeDiagnosticsViewer.Test
{
    [TestFixture]
    public class ExcelMapperFixture
    {
        [Test]
        public void PipeDiagnosticsExcelMapperTest()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PipeDiagnosticsViewer.Test.Data.Objects.xlsx");
            using IMapper<PipeDiagnostic> excelMapper = new ExcelMapper<PipeDiagnostic>(stream, new PipeDiagnosticsFactory());
            IEnumerable<PipeDiagnostic> pipeDiagnostics = excelMapper.GetItems();
            ClassicAssert.AreEqual(pipeDiagnostics.Count(),  4);
        }

        [Test]
        public async Task PipeDiagnosticsCSVMapperTestAsync()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PipeDiagnosticsViewer.Test.Data.Objects.xlsx");
            using IMapper<PipeDiagnostic> excelMapper = new ExcelMapper<PipeDiagnostic>(stream, new PipeDiagnosticsFactory());
            List<PipeDiagnostic> pipeDiagnostics = new List<PipeDiagnostic>();
            await foreach (PipeDiagnostic pipeDiagnostic in excelMapper.GetItemsAsync())
                pipeDiagnostics.Add(pipeDiagnostic);

            ClassicAssert.AreEqual(pipeDiagnostics.Count, 4);
        }
    }
}