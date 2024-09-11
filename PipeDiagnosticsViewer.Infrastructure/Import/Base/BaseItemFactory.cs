namespace PipeDiagnosticsViewer.Infrastructure.Import.Base
{
    public abstract class BaseItemFactory<T> where T : class
    {
        private ParameterParser parameterParser { get; set; }

        protected ParameterParser ParameterParser => parameterParser != null ? parameterParser
                                                                             : throw new ArgumentException("ParameterParser not initialized.");

        public void InitializParameterParser(List<string> parameterNames)
        {
            parameterParser = new ParameterParser(parameterNames);
        }

        public abstract T GetItem(string[] parametersValue);
    }
}