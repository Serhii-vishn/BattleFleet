using BattleFleet.src.PlayerBoard;

namespace BattleFleet.src.Player
{
    class HumanPlayer : Player
    {
        public HumanPlayer() : base() { }

        public HumanPlayer(string playerName)
        {
            this.playerName = playerName;
        }

        private char readColumn()
        {
            Console.Write("Enter column (A-J): ");
            char column = char.ToUpper(Console.ReadKey().KeyChar);

            if (column < 'A' || column > 'J')
                throw new ArgumentException("Invalid column. Please enter a number between A and J.");

            return column;
        }

        private int readRow()
        {
            Console.Write(" row (0-9): ");
            int row;

            if (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row > 9)
                throw new ArgumentException("Invalid row. Please enter a number between 0 and 9.");

            return row;
        }

        private ShipClass selectShip()
        {
            Console.Write($"Available ships:");
            foreach (var kvp in AvailableShips)
            {
                if (kvp.Value > 0)
                    Console.Write($" ({kvp.Value}){kvp.Key}");
            }

            Console.Write("\nShip size: ");
            int shipSize;
            if (!int.TryParse(Console.ReadLine(), out shipSize) ||
                !Enum.IsDefined(typeof(ShipClass), shipSize))
            {
                throw new ArgumentException("Invalid ship size. Please enter a valid ship size.");
            }

            ShipClass selectedShipClass = (ShipClass)shipSize;

            if (AvailableShips.ContainsKey(selectedShipClass) && AvailableShips[selectedShipClass] < 0)
                throw new ArgumentException($"No more {selectedShipClass} ships available.");

            return selectedShipClass;
        }

        private ShipDirection readShipDirection(ShipClass shipClass)
        {
            if (shipClass == ShipClass.ONE_DECK)
            {
                AvailableShips[shipClass]--;
                return ShipDirection.HORIZONTAL;
            }

            Console.Write($"Ship direction {ShipDirection.HORIZONTAL} - 1, {ShipDirection.VERTICAL} - 2: ");
            int direction;
            if (!int.TryParse(Console.ReadLine(), out direction) || !Enum.IsDefined(typeof(ShipDirection), direction))
                throw new ArgumentException("Invalid ship direction. Please enter a valid direction.");

            AvailableShips[shipClass]--;

            return (ShipDirection)Enum.ToObject(typeof(ShipDirection), direction);
        }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override void DrawBoard()
        {
            Console.WriteLine($"Player's board: {playerName}");
            Console.WriteLine(this.ownBoard.Draw());
        }

        public override void PlaceShips()
        {
            try
            {
                char column = readColumn();
                int row = readRow();
                ShipClass shipClass = selectShip();
                ShipDirection shipDirection = readShipDirection(shipClass);

                ownBoard.MovePlaceShip(row, column, shipClass, shipDirection);

                shipPlacement.Add($"{row},{column},{shipClass},{shipDirection}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        public override void PlaceShipsRandom() 
        {
            Random random = new Random();
            
            foreach (var ship in AvailableShips)
            {
                ShipClass shipClass = ship.Key;
                int shipCount = ship.Value;

                while (shipCount > 0)
                {
                    char randomColumn = (char)random.Next(65, 75);
                    int randomRow = random.Next(0, 10);
                    ShipDirection randomShipDirection = (ShipDirection)random.Next(1, 3);

                    if (ownBoard.MovePlaceShip(randomRow, randomColumn, shipClass, randomShipDirection))
                    {
                        shipCount--;
                        shipPlacement.Add($"{randomRow},{randomColumn},{shipClass},{randomShipDirection}");
                    }
                }
            }
        }

        public override void PlaceShipsTemplate(List<string> shipsData)
        {
            try
            {
                foreach (string ship in shipsData) 
                {
                    string[] parts = ship.Split(',');
                    if (parts.Length == 4)
                    {
                        int row = int.Parse(parts[0]);
                        char column = parts[1][0];
                        ShipClass shipClass = (ShipClass)Enum.Parse(typeof(ShipClass), parts[2]);
                        ShipDirection shipDirection = (ShipDirection)Enum.Parse(typeof(ShipDirection), parts[3]);

                        ownBoard.MovePlaceShip(row, column, shipClass, shipDirection);

                        shipPlacement.Add($"{row},{column},{shipClass},{shipDirection}");
                    }
                }               
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        public override bool MakeMove()
        {
            Console.WriteLine("Opponent Board");
            Console.WriteLine(this.opponentBoard.DrawHide());
            try
            {
                Console.WriteLine($"Player's move: {playerName}.");

                Console.Write("Enter: column (A-J): ");
                char column = Console.ReadKey().KeyChar;

                Console.Write(" row (0-9): ");
                int row = int.Parse(Console.ReadLine());

                if (opponentBoard.MoveCheck(row, column))
                    return opponentBoard.MoveShoot(row, column);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }

        public override void ClearBoard()
        {
            shipPlacement.Clear();
            ownBoard.Clear();
        }
    }
}