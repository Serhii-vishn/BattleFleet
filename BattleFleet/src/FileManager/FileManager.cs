namespace BattleFleet.src.FileManager
{
    class FileManager
    {
        protected string mainFolderPath;
        protected string logsFolderPath;
        protected string savesFolderPath;

        public FileManager()
        {
            mainFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");
            logsFolderPath = Path.Combine(mainFolderPath, "logs");
            savesFolderPath = Path.Combine(mainFolderPath, "saves");

            ensureDirectoryExists(mainFolderPath);
            ensureDirectoryExists(logsFolderPath);
            ensureDirectoryExists(savesFolderPath);
        }

        private void ensureDirectoryExists (string directoryPath)
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

        protected string GetFilePathInMainFolder(string fileName)
        {
            string filePath = Path.Combine(mainFolderPath, fileName);

            if (!File.Exists(filePath))
                throw new ArgumentException($"File '{fileName}' does not exist.");

            return filePath;
        }
    }
}