using PipeDiagnosticsViewer.Views;

namespace PipeDiagnosticsViewer.Modules
{
    internal class PipeDiagnosticsModule: IModule
    {
        private IRegionManager regionManager;

        public PipeDiagnosticsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(RegionNames.RootRegion, () => containerProvider.Resolve<PipeDiagnostics>());
        }
    }
}