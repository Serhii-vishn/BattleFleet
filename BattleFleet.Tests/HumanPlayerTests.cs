using BattleFleet.src.Player;
using BattleFleet.src.PlayerBoard;

namespace BattleFleet.Tests
{
    [TestClass]
    public class HumanPlayerTests
    {
        [TestMethod]
        public void DefaultConstructor_InitializesCorrectly()
        {
            HumanPlayer player = new HumanPlayer();

            Assert.AreEqual("player", player.GetPlayerName());
            Assert.IsNotNull(player.AvailableShips);
            Assert.AreEqual(4, player.AvailableShips.Count);
            Assert.AreEqual(1, player.AvailableShips[ShipClass.FIVE_DECK]);
            Assert.AreEqual(2, player.AvailableShips[ShipClass.THREE_DECK]);
            Assert.AreEqual(3, player.AvailableShips[ShipClass.TWO_DECK]);
            Assert.AreEqual(4, player.AvailableShips[ShipClass.ONE_DECK]);
            Assert.IsNotNull(player.GetShipPlacement());
            Assert.AreEqual(0, player.GetShipPlacement().Count);
        }

        [TestMethod]
        public void Constructor_WithPlayerName_InitializesCorrectly()
        {
            HumanPlayer player = new HumanPlayer("TestPlayer");

            Assert.AreEqual("TestPlayer", player.GetPlayerName());
        }
    }
}