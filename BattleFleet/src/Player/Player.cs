using BattleFleet.src.PlayerBoard;

namespace BattleFleet.src.Player
{
    public abstract class Player
    {
        protected string playerName;
        protected Board ownBoard;
        protected Board opponentBoard;
        protected List<string> shipPlacement;
        public Dictionary<ShipClass, int> AvailableShips { get; private set; }

        protected Player()
        {
            playerName = "player";
            ownBoard = new Board();
            opponentBoard = new Board();

            AvailableShips = new Dictionary<ShipClass, int>
            {
                { ShipClass.FIVE_DECK, 1 },
                { ShipClass.THREE_DECK, 2 },
                { ShipClass.TWO_DECK, 3 },
                { ShipClass.ONE_DECK, 4 }
            };
            shipPlacement = new List<string>();
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public void ClearBoard()
        {
            shipPlacement.Clear();
            ownBoard.Clear();
        }

        public int CountAvaliableShips()
        {
            int shipsCount = 0;
            foreach (var kvp in AvailableShips)
            {
                shipsCount += kvp.Value;
            }
            return shipsCount;
        }

        public List<string> GetShipPlacement()
        {
            return shipPlacement;
        }

        public abstract void Initialize(Board ownBoard, Board opponentBoard);   
        public abstract void DrawBoard();
        public abstract void PlaceShips(PlacementMode placementMode);
        public abstract bool MakeMove();
    }
}