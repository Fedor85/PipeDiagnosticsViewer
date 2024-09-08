namespace PipeDiagnosticsViewer.Interfaces
{
    public interface IMapper<T> : IDisposable where T : class
    {
        IEnumerable<T> GetItems();

        IAsyncEnumerable<T> GetItemsAsync();

        void Dispose();
    }
}
