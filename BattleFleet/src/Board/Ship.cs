using System;

namespace BattleFleet.src.PlayerBoard
{
    class Ship
    {
        private ShipClass shipClass;
        private Dictionary<char, int> position;
        private int health;
        private bool isSunk;

        public Ship(ShipClass shipClass, Dictionary<char, int> position)
        {
            this.shipClass = shipClass;
            this.position = position;
            health = (int)shipClass;
            this.isSunk = false;
        }

        public Ship(ShipClass shipClass)
        {
            this.shipClass = shipClass;
            health = (int)shipClass;
            this.isSunk = false;
        }

        private void UpdateStatus()
        {
            isSunk = true;
        }

        public bool IsSunk()
        {
            return isSunk;
        }

        public void hit()
        {
            if (health > 0)
            {
                int damage = 1;
                health -= damage;

                if (health == 0)
                {
                    UpdateStatus();
                }
            }
            else
            {
                throw new ShipAlreadySunkException();
            }
        }
        
        public void setPosition(Dictionary<char, int> position)
        {
            this.position = position;
        }

        public Dictionary<char, int> getPosition()
        {
            return position;
        }

        public int getHealth()
        {
            return health;
        }

        public ShipClass getShipClass()
        { 
            return shipClass; 
        }
    }
}

public class ShipAlreadySunkException : Exception
{
    public ShipAlreadySunkException() : base("Cannot hit a ship that is already sunk."){}
}