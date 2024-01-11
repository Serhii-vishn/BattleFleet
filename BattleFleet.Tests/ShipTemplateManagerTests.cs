using BattleFleet.src.FileManager;

namespace BattleFleet.Tests
{
    [TestClass]
    public class ShipTemplateManagerTests
    {
        [TestMethod]
        public void GetTemplateNames_ReturnsTemplateNames()
        {
            ShipTemplateManager templateManager = new ShipTemplateManager();

            List<string> templateNames = templateManager.GetTemplateNames();

            CollectionAssert.Contains(templateNames, "Master");
            CollectionAssert.Contains(templateNames, "Arcade");
            CollectionAssert.Contains(templateNames, "Redis");
            CollectionAssert.Contains(templateNames, "GLORY_FOR_UKRAINE");
        }

        [TestMethod]
        public void GetTemplateByName_ReturnsCorrectTemplate()
        {
            ShipTemplateManager templateManager = new ShipTemplateManager();

            List<string> template = templateManager.GetTemplateByName("Master");

            Assert.IsNotNull(template);
            CollectionAssert.Contains(template, "5,A,FIVE_DECK,VERTICAL");
            CollectionAssert.Contains(template, "8,D,ONE_DECK,HORIZONTAL");
        }
    }
}