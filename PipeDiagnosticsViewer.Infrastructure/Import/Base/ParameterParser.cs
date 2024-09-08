namespace PipeDiagnosticsViewer.Infrastructure.Import.Base
{
    public class ParameterParser
    {
        private List<string> parameterNames;

        public ParameterParser(List<string> parameterNames)
        {
            this.parameterNames = parameterNames;
        }

        public string GetParameterValue(string[] parametersValue, string parameterName)
        {
            int index = parameterNames.IndexOf(parameterName);
            if (index == -1)
                throw new ArgumentException($"{parameterName} not found.");

            return parametersValue[index];
        }

        public T GetParameterValue<T>(string[] parametersValue, string parameterName)
        {
            return (T)Convert.ChangeType(GetParameterValue(parametersValue, parameterName), typeof(T));
        }
    }
}
