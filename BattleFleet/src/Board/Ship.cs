using System;

namespace BattleFleet.src.Board
{
    class Ship
    {
        private ShipClass shipClass;
        private Dictionary<char, int> position;
        private bool isSunk;

        Ship(ShipClass shipClass, Dictionary<char, int> position)
        {
            this.shipClass = shipClass;
            this.position = position;
            this.isSunk = false;
        }

        public void updateStatus()
        {
            isSunk = true;
        }
    }
}