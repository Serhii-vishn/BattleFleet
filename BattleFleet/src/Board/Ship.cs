namespace BattleFleet.src.PlayerBoard
{
    public class Ship
    {
        private readonly ShipClass _shipClass;
        private readonly ShipDirection _shipDirection;
        private Dictionary<char, int> _position;
        private int _health;
        private bool _isShipSunk;

        public Ship(ShipClass shipClass, Dictionary<char, int> position, ShipDirection shipDirection)
        {
            _shipClass = shipClass;
            _position = position;
            _shipDirection = shipDirection;
            _health = (int)shipClass;
            _isShipSunk = false;
        }

        public bool IsSunk()
        {
            return _isShipSunk;
        }

        public void Hit()
        {
            if (_health > 0)
            {
                _health -= 1;

                if (_health <= 0)
                    UpdateStatus();
            }
        }

        public void SetPosition(Dictionary<char, int> position)
        {
            _position = position;
        }

        public Dictionary<char, int> GetPosition()
        {
            return _position;
        }

        public ShipClass GetShipClass()
        {
            return _shipClass;
        }

        public ShipDirection GetDirection()
        {
            return _shipDirection;
        }

        private void UpdateStatus()
        {
            _isShipSunk = true;
        }
    }
}