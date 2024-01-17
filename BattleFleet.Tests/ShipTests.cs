using BattleFleet.src.PlayerBoard;

namespace BattleFleet.Tests
{
    [TestClass]
    public sealed class ShipTests
    {
        [TestMethod]
        public void ShipClass_ContainsExpectedValues()
        {
            var expectedValues = new ShipClass[]
            {
                ShipClass.ONE_DECK,
                ShipClass.TWO_DECK,
                ShipClass.THREE_DECK,
                ShipClass.FIVE_DECK
            };

            CollectionAssert.AreEqual(expectedValues, Enum.GetValues(typeof(ShipClass)));
        }

        [TestMethod]
        [DataRow(ShipClass.ONE_DECK, 1)]
        [DataRow(ShipClass.TWO_DECK, 2)]
        [DataRow(ShipClass.THREE_DECK, 3)]
        [DataRow(ShipClass.FIVE_DECK, 5)]
        public void ShipClass_ContainsExpectedHealthShip(ShipClass shipclass, int expectedShipHealth)
        {
            int health = (int)shipclass;

            Assert.AreEqual(health, expectedShipHealth);
        }

        [TestMethod]
        public void ShipDirection_ContainsExpectedValues()
        {
            var expectedValues = new ShipDirection[]
            {
                ShipDirection.HORIZONTAL,
                ShipDirection.VERTICAL,
            };

            CollectionAssert.AreEqual(expectedValues, Enum.GetValues(typeof(ShipDirection)));
        }

        [TestMethod]
        [DataRow(ShipDirection.HORIZONTAL, 1)]
        [DataRow(ShipDirection.VERTICAL, 2)]
        public void ShipDirection_ContainsExpectedNumbers(ShipDirection shipDirection, int expectedNumbers)
        {
            int direction = (int)shipDirection;

            Assert.AreEqual(direction, expectedNumbers);
        }

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