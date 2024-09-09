using System.Reflection;

namespace PipeDiagnosticsViewer.Test
{
    public static class TestHelper
    {
        public static string GetTempFilePath(string resourcesTestFileName)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcesTestFileName);
            string tempPathDirectory = Path.GetTempPath();
            string extension = Path.GetExtension(resourcesTestFileName);
            string tempFilePath = GetTempFilePath(tempPathDirectory, extension);

            while (File.Exists(tempFilePath))
                tempFilePath = GetTempFilePath(tempPathDirectory, extension);

            using FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            return tempFilePath;
        }

        private static string GetTempFilePath(string tempPathDirectory, string extension)
        {
            string randomFileName = Path.GetRandomFileName();
            string randomFileNameExtension = Path.ChangeExtension(randomFileName, extension);
            return Path.Combine(tempPathDirectory, randomFileNameExtension);
        }
    }
}