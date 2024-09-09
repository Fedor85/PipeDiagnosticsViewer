using PipeDiagnosticsViewer.Modules;
using PipeDiagnosticsViewer.Views;
using System.Windows;
using PipeDiagnosticsViewer.Infrastructure;
using PipeDiagnosticsViewer.Interfaces;
using PipeDiagnosticsViewer.Services;

namespace PipeDiagnosticsViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IInteractionRequestService, InteractionRequestService>();
            containerRegistry.RegisterScoped<IPipeDiagnosticFromFileService, PipeDiagnosticFromFileService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(PipeDiagnosticsModule), InitializationMode.OnDemand);
        }
    }
}
