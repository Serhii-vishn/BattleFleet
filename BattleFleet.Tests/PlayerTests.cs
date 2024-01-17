using BattleFleet.src.Player;
using BattleFleet.src.PlayerBoard;
using System.Numerics;

namespace BattleFleet.Tests
{
    [TestClass]
    public sealed class PlayerTests
    {
        [TestMethod]
        public void DefaultConstructor_InitializesCorrectly()
        {
            Player player = new MockPlayer();

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
        public void SetPlayerName_ChangesPlayerName()
        {
            Player player = new MockPlayer();

            player.SetPlayerName("NewPlayer");

            Assert.AreEqual("NewPlayer", player.GetPlayerName());
        }

        [TestMethod]
        public void ClearBoard_ResetsShipPlacementAndOwnBoard()
        {
            Player player = new MockPlayer();
            player.GetShipPlacement().Add("1,1,ONE_DECK,HORIZONTAL");

            player.ClearBoard();

            Assert.AreEqual(0, player.GetShipPlacement().Count);
        }

        [TestMethod]
        public void CountAvailableShips_ReturnsCorrectCount()
        {
            Player player = new MockPlayer();

            int count = player.CountAvaliableShips();

            Assert.AreEqual(10, count);
        }
    }

    class MockPlayer : Player
    {
        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawBoard()
        {
            throw new System.NotImplementedException();
        }

        public override void PlaceShips(PlacementMode placementMode)
        {
            throw new System.NotImplementedException();
        }

        public override bool MakeMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
