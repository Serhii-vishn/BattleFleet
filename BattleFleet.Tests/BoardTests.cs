using BattleFleet.src.PlayerBoard;

namespace BattleFleet.Tests
{
    [TestClass]
    public sealed class BoardTests
    {
        [TestMethod]
        public void InitializeBoard_CorrectInitialization()
        {
            Board board = new Board();

            string result = board.Draw();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("A | B | C | D | E | F | G | H | I | J |"));
        }

        [TestMethod]
        public void MovePlaceShip_ValidPlacement()
        {
            Board board = new Board();
            ShipClass shipClass = ShipClass.ONE_DECK;
            ShipDirection shipDirection = ShipDirection.HORIZONTAL;

            bool result = board.MovePlaceShip(1, 'A', shipClass, shipDirection);

            Assert.IsTrue(result);
            Assert.AreEqual(1, board.GetAliveShipsCount());
        }

        [TestMethod]
        public void MoveCheck_ValidMove()
        {
            Board board = new Board();

            bool result = board.MoveCheck(1, 'A');

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MoveShoot_HitShip()
        {
            Board board = new Board();
            ShipClass shipClass = ShipClass.ONE_DECK;
            ShipDirection shipDirection = ShipDirection.VERTICAL;
            board.MovePlaceShip(1, 'A', shipClass, shipDirection);

            bool result = board.MoveShoot(1, 'A');

            Assert.IsTrue(result);
            Assert.AreEqual(0, board.GetAliveShipsCount());
        }

        [TestMethod]
        public void MoveShoot_MissedShot()
        {
            Board board = new Board();

            bool result = board.MoveShoot(1, 'A');

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAliveShipsCount_ShouldReturnCorrectCount()
        {
            Board board = new Board();
            ShipClass shipClass = ShipClass.THREE_DECK;
            ShipDirection shipDirection = ShipDirection.VERTICAL;
            board.MovePlaceShip(1, 'A', shipClass, shipDirection);

            int result = board.GetAliveShipsCount();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Clear_ShouldClearShips()
        {
            Board board = new Board();
            ShipClass shipClass = ShipClass.TWO_DECK;
            ShipDirection shipDirection = ShipDirection.HORIZONTAL;
            board.MovePlaceShip(1, 'A', shipClass, shipDirection);

            board.Clear();
            int result = board.GetAliveShipsCount();

            Assert.AreEqual(0, result);
        }
    }
}
