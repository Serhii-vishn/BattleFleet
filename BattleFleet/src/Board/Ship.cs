﻿namespace BattleFleet.src.PlayerBoard
{
    class Ship
    {
        private readonly ShipClass shipClass;
        private readonly ShipDirection shipDirection;
        private Dictionary<char, int> position;
        private int health;
        private bool isShipSunk;

        public Ship(ShipClass shipClass, Dictionary<char, int> position, ShipDirection shipDirection)
        {
            this.shipClass = shipClass;
            this.position = position;
            this.shipDirection = shipDirection;
            health = (int)shipClass;
            isShipSunk = false;
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
                health -= 1;

                if (health <= 0)
                    updateStatus();
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