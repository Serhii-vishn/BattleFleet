namespace BattleFleet.src.FileManager
{
    public class FileManager
    {
        protected string mainFolderPath;
        protected string logsFolderPath;
        protected string savesFolderPath;

        public FileManager()
        {
            mainFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");
            logsFolderPath = Path.Combine(mainFolderPath, "logs");
            savesFolderPath = Path.Combine(mainFolderPath, "saves");

            EnsureDirectoryExists(mainFolderPath);
            EnsureDirectoryExists(logsFolderPath);
            EnsureDirectoryExists(savesFolderPath);
        }

        protected string GetFilePathInMainFolder(string fileName)
        {
            string filePath = Path.Combine(mainFolderPath, fileName);

            if (!File.Exists(filePath))
                throw new ArgumentException($"File '{fileName}' does not exist.");

            return filePath;
        }

        private static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating directory {directoryPath}: {ex.Message}");
                }
            }
        }
    }
}