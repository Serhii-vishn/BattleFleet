using BattleFleet.src.FileManager;

namespace BattleFleet.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void Constructor_CreatesDirectories()
        {
            string mainFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");
            string logsFolderPath = Path.Combine(mainFolderPath, "logs");
            string savesFolderPath = Path.Combine(mainFolderPath, "saves");

            var fileManager = new FileManager();

            Assert.IsTrue(Directory.Exists(mainFolderPath));
            Assert.IsTrue(Directory.Exists(logsFolderPath));
            Assert.IsTrue(Directory.Exists(savesFolderPath));
        }
    }
}
