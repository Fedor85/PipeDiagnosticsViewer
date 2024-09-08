using PipeDiagnosticsViewer.Infrastructure.Import.Base;
using PipeDiagnosticsViewer.Interfaces;
using Spire.Xls;

namespace PipeDiagnosticsViewer.Infrastructure.Import
{
    public class ExcelMapper<T> : IMapper<T> where T : class
    {
        private Worksheet sheet;

        private BaseItemFactory<T> itemsFactory;
        public ExcelMapper(Stream stream, BaseItemFactory<T> itemsFactory)
        {
            this.itemsFactory = itemsFactory;
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            sheet = workbook.Worksheets[0];
            itemsFactory.InitializParameterParser(GetParameterNames(sheet.Rows[0]));
        }

        private List<string> GetParameterNames(CellRange sheetRow)
        {
            return sheetRow.CellList.Select(item => item.Text).ToList();
        }

        public IEnumerable<T> GetItems()
        {
            List<T> items = new List<T>();
            for (int i = 1; i < sheet.Rows.Length; i++)
                items.Add(GetItem(i));

            return items;
        }

        public async IAsyncEnumerable<T> GetItemsAsync()
        {
            for (int i = 1; i < sheet.Rows.Length; i++)
                yield return GetItem(i);
        }

        private T GetItem(int rowIndex)
        {
            string[] parametersValue = sheet.Rows[rowIndex].CellList.Select(item => item.Value).ToArray();
            return itemsFactory.GetItem(parametersValue);
        }

        public void Dispose()
        {
            sheet.Dispose();
        }
    }
}