using PipeDiagnosticsViewer.Models;

namespace PipeDiagnosticsViewer.Infrastructure.Import.Base
{
    public class PipeDiagnosticsFactory : BaseItemFactory<PipeDiagnostic>
    {
        public override PipeDiagnostic GetItem(string[] parametersValue)
        {
            CheckInitialized();
            PipeDiagnostic pipeDiagnostic = new PipeDiagnostic();
            pipeDiagnostic.Name = parameterParser.GetParameterValue(parametersValue, "Name");
            pipeDiagnostic.Distance = parameterParser.GetParameterValue<double>(parametersValue, "Distance");
            pipeDiagnostic.Angle = parameterParser.GetParameterValue<double>(parametersValue, "Angle");
            pipeDiagnostic.Width = parameterParser.GetParameterValue<double>(parametersValue, "Width");
            pipeDiagnostic.Height = parameterParser.GetParameterValue<double>(parametersValue, "Hegth");
            pipeDiagnostic.IsDefect = parameterParser.GetParameterValue(parametersValue, "IsDefect").Trim().ToLower().Equals("yes");
            return pipeDiagnostic;
        }
    }
}