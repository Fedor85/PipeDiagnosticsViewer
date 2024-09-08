using PipeDiagnosticsViewer.Modules;
using System.Windows.Input;

namespace PipeDiagnosticsViewer.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private IModuleManager moduleManager;

        private IRegionManager regionManager;

        public ICommand ClosingCommand { get; }

        public ICommand LoadedCommand { get; }

        public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager)
        {
            this.moduleManager = moduleManager;
            this.regionManager = regionManager;
            LoadedCommand = new DelegateCommand(Loaded);
            ClosingCommand = new DelegateCommand(Closing);
        }

        private void Loaded()
        {
            moduleManager.LoadModule(typeof(PipeDiagnosticsModule).Name);
        }

        private void Closing()
        {
            foreach (IRegion region in regionManager.Regions)
            {
                foreach (object view in region.Views)
                    region.Deactivate(view);
            }
        }
    }
}
