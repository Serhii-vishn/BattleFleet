using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    abstract class Player
    {
        protected string playerName;
        protected Board ownBoard;
        protected Board opponentBoard;
        public Dictionary<ShipClass, int> AvailableShips { get; private set; }

        public Player()
        {
            this.playerName = "player";
            AvailableShips = new Dictionary<ShipClass, int>
            {
                { ShipClass.FIVE_DECK, 1 },
                { ShipClass.THREE_DECK, 2 },
                { ShipClass.TWO_DECK, 3 },
                { ShipClass.ONE_DECK, 4 }
            };
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public abstract void Initialize(Board ownBoard, Board opponentBoard);
        public abstract void DrawBoard();
        public abstract void PlaceShips();
        public abstract bool MakeMove();
        public int CountAvaliableShips()
        {
            int shipsCount = 0;
            foreach (var kvp in AvailableShips)
            {
                shipsCount += kvp.Value;
            }
            return shipsCount;
        }
    }
}