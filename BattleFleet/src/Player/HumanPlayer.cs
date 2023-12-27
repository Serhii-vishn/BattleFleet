using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class HumanPlayer : Player
    {
        public Dictionary<ShipClass, int> AvailableShips { get; private set; }

        public HumanPlayer() : base() {}

        public HumanPlayer(string playerName)
        {
            this.playerName = playerName;
            AvailableShips = new Dictionary<ShipClass, int>
            {
                { ShipClass.FIVE_DECK, 1 },
                { ShipClass.THREE_DECK, 2 },
                { ShipClass.TWO_DECK, 3 },
                { ShipClass.ONE_DECK, 4 }
            };
        }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override void DrawBoard()
        {
            Console.WriteLine($"Player's board: {playerName}");
            Console.WriteLine(this.ownBoard.DrawBoard());
        }

        public override void PlaceShips()
        {
            char column;
            Console.Write("Enter: column (A-J): ");
            column = char.ToUpper(Console.ReadKey().KeyChar);
            if (column < 'A' || column > 'J')
                throw new ArgumentException("Invalid column. Please enter a number between A and J.");

            Console.Write(" row (0-9): ");
            int row;
            if (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row > 9)
                throw new ArgumentException("Invalid row. Please enter a number between 0 and 9.");
         
            ShipClass shipClass = selectShip();

            int direction = 1;

            if (shipClass != ShipClass.ONE_DECK)
            {
                Console.Write($"Ship direction {ShipDirection.HORIZONTAL} - 0, {ShipDirection.VERTICAL} - 1: ");

                if (!int.TryParse(Console.ReadLine(), out direction) ||
                    !Enum.IsDefined(typeof(ShipDirection), direction))
                {
                    throw new ArgumentException("Invalid ship direction. Please enter a valid direction.");
                }
            }
            ShipDirection shipDirection = (ShipDirection)Enum.ToObject(typeof(ShipDirection), direction);

            ownBoard.MovePlaceShip(row, column, shipClass, shipDirection);
        }

        private ShipClass selectShip()
        {
            Console.Write($"Ship size in cells:" );
            foreach (var kvp in AvailableShips)
            {
                Console.Write($" ({kvp.Value}){kvp.Key}");
            }

            Console.Write("Ship size: ");
            int shipSize;
            if (!int.TryParse(Console.ReadLine(), out shipSize) ||
                !Enum.IsDefined(typeof(ShipClass), shipSize))
            {
                throw new ArgumentException("Invalid ship size. Please enter a valid ship size.");
            }

            ShipClass selectedShipClass = (ShipClass)shipSize;

            if (AvailableShips.ContainsKey(selectedShipClass) && AvailableShips[selectedShipClass] > 0)
                AvailableShips[selectedShipClass]--;
            else
                throw new ArgumentException($"No more {selectedShipClass} ships available.");

            return selectedShipClass;
        }

        public override bool MakeMove()
        {
            Console.WriteLine("Opponent Board");
            Console.WriteLine(this.opponentBoard.DrawHideBoard());
            try
            {
                Console.WriteLine($"Player's move: {playerName}.");

                Console.Write("Enter: column (A-J): ");
                char column = Console.ReadKey().KeyChar;

                Console.Write(" row (0-9): ");
                int row = int.Parse(Console.ReadLine());              

                if (opponentBoard.CheckMove(row, column))
                    return opponentBoard.MoveShoot(row, column);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }
    }
}