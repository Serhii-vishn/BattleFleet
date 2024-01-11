using BattleFleet.src.PlayerBoard;

namespace BattleFleet.Tests
{
    [TestClass]
    public sealed class ShipTests
    {
        [TestMethod]
        public void ShipInitialization()
        {
            ShipClass shipClass = ShipClass.FIVE_DECK;
            ShipDirection shipDirection = ShipDirection.HORIZONTAL;
            Dictionary<char, int> position = new Dictionary<char, int> { { 'A', 5 }};

            Ship ship = new Ship(shipClass, position, shipDirection);


            Assert.AreEqual(shipClass, ship.GetShipClass());
            Assert.AreEqual(shipDirection, ship.GetDirection());
            Assert.AreEqual(position, ship.GetPosition());
            Assert.IsFalse(ship.IsSunk());
        }

        [TestMethod]
        public void ShipHitReducesHealthIsSunk()
        {
            Ship ship = new Ship(ShipClass.ONE_DECK, new Dictionary<char, int> { { 'A', 0 } }, ShipDirection.VERTICAL);

            ship.Hit();

            Assert.IsTrue(ship.IsSunk());
        }

        [TestMethod]
        public void ShipSetPositionUpdatesPosition()
        {
            Ship ship = new Ship(ShipClass.TWO_DECK, new Dictionary<char, int> { { 'D', 2 }}, ShipDirection.HORIZONTAL);
            Dictionary<char, int> newPosition = new Dictionary<char, int> { { 'B', 1 }};

            ship.SetPosition(newPosition);

            Assert.AreEqual(newPosition, ship.GetPosition());
        }
    }
}