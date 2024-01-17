using BattleFleet.src.Player;
using BattleFleet.src.PlayerBoard;

namespace BattleFleet.Tests
{
    [TestClass]
    public class ComputerPlayerTests
    {
        [TestMethod]
        public void DefaultConstructor_InitializesCorrectly()
        {
            ComputerPlayer computerPlayer = new ComputerPlayer();

            Assert.AreEqual("player", computerPlayer.GetPlayerName());
            Assert.IsNotNull(computerPlayer.AvailableShips);
            Assert.AreEqual(4, computerPlayer.AvailableShips.Count);
            Assert.AreEqual(1, computerPlayer.AvailableShips[ShipClass.FIVE_DECK]);
            Assert.AreEqual(2, computerPlayer.AvailableShips[ShipClass.THREE_DECK]);
            Assert.AreEqual(3, computerPlayer.AvailableShips[ShipClass.TWO_DECK]);
            Assert.AreEqual(4, computerPlayer.AvailableShips[ShipClass.ONE_DECK]);
            Assert.IsNotNull(computerPlayer.GetShipPlacement());
            Assert.AreEqual(0, computerPlayer.GetShipPlacement().Count);
        }

        [TestMethod]
        public void Constructor_WithPlayerName_InitializesCorrectly()
        {
            ComputerPlayer computerPlayer = new ComputerPlayer("TestPlayer");

            Assert.AreEqual("TestPlayer", computerPlayer.GetPlayerName());
        }
    }
}