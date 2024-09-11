using PipeDiagnosticsViewer.Infrastructure.Import.Base;
using PipeDiagnosticsViewer.Models;

namespace PipeDiagnosticsViewer.Infrastructure.Import
{
    public class PipeDiagnosticsFactory : BaseItemFactory<PipeDiagnostic>
    {
        public override PipeDiagnostic GetItem(string[] parametersValue)
        {
            PipeDiagnostic pipeDiagnostic = new PipeDiagnostic();
            pipeDiagnostic.Name = ParameterParser.GetParameterValue(parametersValue, "name");
            pipeDiagnostic.Distance = ParameterParser.GetParameterValue<double>(parametersValue, "distance");
            pipeDiagnostic.Angle = ParameterParser.GetParameterValue<double>(parametersValue, "angle");
            pipeDiagnostic.Width = ParameterParser.GetParameterValue<double>(parametersValue, "width");
            pipeDiagnostic.Height = ParameterParser.GetParameterValue<double>(parametersValue, "hegth");
            pipeDiagnostic.IsDefect = ParameterParser.GetParameterValue(parametersValue, "isdefect").Trim().ToLower().Equals("yes");
            return pipeDiagnostic;
        }
    }
}