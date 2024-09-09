namespace PipeDiagnosticsViewer.Infrastructure
{
    public static class FileHelper
    {
        public static string GetExtensionWihtoutPoint(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            if (String.IsNullOrEmpty(extension))
                throw new ArgumentException("File path is empty.");

            return extension.Replace(".", String.Empty).Trim().ToLower();
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }
    }
}