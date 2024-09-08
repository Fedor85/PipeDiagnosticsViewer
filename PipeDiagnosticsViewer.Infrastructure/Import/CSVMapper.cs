using Microsoft.VisualBasic.FileIO;
using PipeDiagnosticsViewer.Infrastructure.Import.Base;
using PipeDiagnosticsViewer.Interfaces;

namespace PipeDiagnosticsViewer.Infrastructure.Import
{
    public class CSVMapper<T> : IMapper<T> where T : class
    {
        private TextFieldParser fieldParser;

        private BaseItemFactory<T> itemsFactory;

        private const char separator = ';';

        public CSVMapper(Stream stream, BaseItemFactory<T> itemsFactory)
        {
            fieldParser = new TextFieldParser(stream);
            this.itemsFactory = itemsFactory;

            fieldParser.SetDelimiters(separator.ToString());
            fieldParser.HasFieldsEnclosedInQuotes = true;

            itemsFactory.InitializParameterParser(GetParameterNames(fieldParser.ReadLine()));
        }

        public IEnumerable<T> GetItems()
        {
            List<T> items = new List<T>();
            while (!fieldParser.EndOfData)
                items.Add(GetItem());

            return items;
        }

        public async IAsyncEnumerable<T> GetItemsAsync()
        {
            while (!fieldParser.EndOfData)
                yield return GetItem();
        }

        private T GetItem()
        {
            string[] parametersValue = fieldParser.ReadFields();
            return itemsFactory.GetItem(parametersValue);
        }

        private List<string> GetParameterNames(string parametersNameLine)
        {
            List<string> parameterNumbers = new List<string>();
            foreach (string parameterName in parametersNameLine.Split(separator))
                parameterNumbers.Add(parameterName.Trim());

            return parameterNumbers;
        }

        public void Dispose()
        {
            fieldParser.Close();
        }
    }
}
