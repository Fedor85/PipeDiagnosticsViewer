namespace PipeDiagnosticsViewer.Interfaces
{
    public interface IInteractionRequestService
    {
        void ShowMessage(string message);

        void ShowError(string message);

        string OpenFile(params string[] extensions);
    }
}
