namespace PipeDiagnosticsViewer.Infrastructure.Import.Base
{
    public abstract class BaseItemFactory<T> where T : class
    {
        protected ParameterParser parameterParser { get; set; }

        public void InitializParameterParser(List<string> parameterNames)
        {
            parameterParser = new ParameterParser(parameterNames);
        }

        public abstract T GetItem(string[] parametersValue);

        protected void CheckInitialized()
        {
            if (parameterParser == null)
                throw new ArgumentException("ParameterParser not initialized.");
        }
    }
}