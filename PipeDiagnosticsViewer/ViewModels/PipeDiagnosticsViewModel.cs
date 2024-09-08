﻿using PipeDiagnosticsViewer.Interfaces;
using System.Windows.Input;
using PipeDiagnosticsViewer.Models;
using System.Collections.ObjectModel;
using PipeDiagnosticsViewer.Infrastructure;

namespace PipeDiagnosticsViewer.ViewModels
{
    public class PipeDiagnosticsViewModel: ActiveAwareBaseModel
    {
        private IPipeDiagnosticFromFileService pipeDiagnosticFromFileService;

        private IInteractionRequestService interactionRequestService;

        private string fileName;

        private ObservableCollection<PipeDiagnostic> pipeDiagnostics;

        public ICommand OpenFileCommand { get; }

        public string FileName
        {
            get { return fileName; }
            set { SetProperty(ref fileName, value); }
        }

        public ObservableCollection<PipeDiagnostic> PipeDiagnostics
        {
            get { return pipeDiagnostics; }
            set { SetProperty(ref pipeDiagnostics, value); }
        }

        public PipeDiagnosticsViewModel(IPipeDiagnosticFromFileService pipeDiagnosticFromFileService,
                                        IInteractionRequestService interactionRequestService)
        {
            this.pipeDiagnosticFromFileService = pipeDiagnosticFromFileService;
            this.interactionRequestService = interactionRequestService;

            OpenFileCommand = new DelegateCommand(ImportPipeDiagnostics);
            PipeDiagnostics = new ObservableCollection<PipeDiagnostic>();
        }

        protected override void DeActivate()
        {
            FileName = String.Empty;
            PipeDiagnostics.Clear();
        }

        private void ImportPipeDiagnostics()
        {
            string filePath = interactionRequestService.OpenFile(pipeDiagnosticFromFileService.SupportedExtensions);
            if(String.IsNullOrEmpty(filePath))
                return;

            FileName = String.Empty;
            PipeDiagnostics.Clear();
            DoImportPipeDiagnostics(filePath);
        }

        private async Task DoImportPipeDiagnostics(string filePath)
        {
            try
            {
                await ImportPipeDiagnostics(filePath);
                FileName = FileHelper.GetFileName(filePath);
            }
            catch (Exception e)
            {
                interactionRequestService.ShowError(e.Message);
            }
        }

        private async Task ImportPipeDiagnostics(string filePath)
        {
            await foreach(PipeDiagnostic pipeDiagnostic in pipeDiagnosticFromFileService.GetItemsAsync(filePath))
                PipeDiagnostics.Add(pipeDiagnostic);
        }
    }
}