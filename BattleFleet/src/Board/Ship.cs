namespace BattleFleet.src.PlayerBoard
{
    class Ship
    {
        private ShipClass shipClass;
        private ShipDirection shipDirection;
        private Dictionary<char, int> position;
        private int health;
        private bool isShipSunk;

        public Ship(ShipClass shipClass, Dictionary<char, int> position, ShipDirection shipDirection)
        {
            this.shipClass = shipClass;
            this.position = position;
            this.shipDirection = shipDirection;
            this.health = (int)shipClass;
            this.isShipSunk = false;
        }

        public Ship(ShipClass shipClass)
        {
            this.shipClass = shipClass;
            health = (int)shipClass;
            this.isShipSunk = false;
        }

        private void updateStatus()
        {
            isShipSunk = true;
        }

        public bool IsSunk()
        {
            return isShipSunk;
        }

        public void Hit()
        {
            if (health > 0)
            {
                int damage = 1;
                health -= damage;

                if (health == 0)
                    isShipSunk = true;

            }
            else
            {
                throw new ArgumentException("Cannot hit a ship that is already sunk.");
            }
        }

        public void SetPosition(Dictionary<char, int> position)
        {
            this.position = position;
        }

        public Dictionary<char, int> GetPosition()
        {
            return position;
        }

        public int GetHealth()
        {
            return health;
        }

        public ShipClass GetShipClass()
        {
            return shipClass;
        }

        public ShipDirection GetDirection()
        {
            return shipDirection;
        }
    }
}