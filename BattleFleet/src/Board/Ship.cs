using System;

namespace BattleFleet.src.PlayerBoard
{
    class Ship
    {
        private ShipClass shipClass;
        private Dictionary<char, int> position;
        private int health;
        private bool isShipSunk;

        public Ship(ShipClass shipClass, Dictionary<char, int> position)
        {
            this.shipClass = shipClass;
            this.position = position;
            health = (int)shipClass;
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
                {
                    updateStatus();
                }
            }
            else
            {
                throw new ShipAlreadySunkException();
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
    }
}

public class ShipAlreadySunkException : Exception
{
    public ShipAlreadySunkException() : base("Cannot hit a ship that is already sunk."){}
}