using BattleFleet.src.FileManager;
using BattleFleet.src.PlayerBoard;

namespace BattleFleet.src.Player
{
    public class HumanPlayer : Player
    {
        public HumanPlayer() : base() { }

        public HumanPlayer(string playerName)
        {
            this.playerName = playerName;
        }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override void DrawBoard()
        {
            Console.WriteLine($"Player's board: {playerName}");
            Console.WriteLine(this.ownBoard.GetBoardToString());
        }

        public override void PlaceShips(PlacementMode placementMode)
        {
            switch (placementMode)
            {
                case PlacementMode.RANDOM:
                    {
                        PlaceShipsRandom();
                        break;
                    }
                case PlacementMode.MANUAL:
                    {
                        PlaceShipsManual();
                        break;
                    }
                case PlacementMode.TEMPLATE:
                    {
                        PlaceShipsTemplate();
                        break;
                    }
                default:
                    break;
            }
        }

        public override bool MakeMove()
        {
            Console.WriteLine("Opponent Board");
            Console.WriteLine(opponentBoard.DrawHide());
            try
            {
                bool isSuccses = false;
                Console.WriteLine($"Player's move: {playerName}");

                char column = ReadColumn();
                int row = ReadRow();

                if (opponentBoard.MoveCheck(row, column))
                {
                    isSuccses = opponentBoard.MoveShoot(row, column);
                }

                if (isSuccses)
                    Console.WriteLine("Nice shoot!");
                else
                    Console.WriteLine("Mised!");

                return isSuccses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                return true;
            }
        }

        private void PlaceShipsManual()
        {
            char column = ReadColumn();
            int row = ReadRow();
            ShipClass shipClass = SelectShip();
            ShipDirection shipDirection = ReadShipDirection(shipClass);

            if (ownBoard.MovePlaceShip(row, column, shipClass, shipDirection))
            {
                shipPlacement.Add($"{row},{column},{shipClass},{shipDirection}");
                AvailableShips[shipClass]--;
            }
        }

        private void PlaceShipsRandom()
        {
            Random random = new();

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

        private void PlaceShipsTemplate()
        {
            var templateManager = new ShipTemplateManager();

            Console.WriteLine("\n\t\t\tList of all templates:");
            var templNames = templateManager.GetTemplateNames();
            int i = 0;
            foreach (var name in templNames)
            {
                i++;
                Console.WriteLine($"\t\t\t\t{i}. {name}");
            }

            Console.Write("\t\t\tSelect by number: ");
            if (!int.TryParse(Console.ReadLine(), out int templNumber) || templNumber < 1 || templNumber > templNames.Count)
                throw new ArgumentException("Invalid data. Please enter a valid template number.");

            var shipTemplate = templateManager.GetTemplateByName(templNames[templNumber - 1]);

            foreach (string ship in shipTemplate)
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

        private static char ReadColumn()
        {
            Console.Write("Enter column (A-J): ");
            char column = char.ToUpper(Console.ReadKey().KeyChar);

            if (column < 'A' || column > 'J')
                throw new ArgumentException("Invalid column. Please enter a number between A and J.");

            return column;
        }

        private static int ReadRow()
        {
            Console.Write(" row (0-9): ");

            if (!int.TryParse(Console.ReadLine(), out int row) || row < 0 || row > 9)
                throw new ArgumentException("Invalid row. Please enter a number between 0 and 9.");

            return row;
        }

        private ShipClass SelectShip()
        {
            Console.Write($"Available ships:");
            foreach (var kvp in AvailableShips)
            {
                if (kvp.Value > 0)
                    Console.Write($" ({kvp.Value}){kvp.Key}");
            }

            Console.Write("\nShip size: ");

            if (!int.TryParse(Console.ReadLine(), out int shipSize) || !Enum.IsDefined(typeof(ShipClass), shipSize))
                throw new ArgumentException("Invalid ship size. Please enter a valid ship size.");

            ShipClass selectedShipClass = (ShipClass)shipSize;

            if (AvailableShips.ContainsKey(selectedShipClass) && AvailableShips[selectedShipClass] < 0)
                throw new ArgumentException($"No more {selectedShipClass} ships available.");

            return selectedShipClass;
        }

        private static ShipDirection ReadShipDirection(ShipClass shipClass)
        {
            if (shipClass == ShipClass.ONE_DECK)
                return ShipDirection.HORIZONTAL;

            Console.Write($"Ship direction {ShipDirection.HORIZONTAL} - 1, {ShipDirection.VERTICAL} - 2: ");

            if (!int.TryParse(Console.ReadLine(), out int direction) || !Enum.IsDefined(typeof(ShipDirection), direction))
                throw new ArgumentException("Invalid ship direction. Please enter a valid direction.");

            return (ShipDirection)Enum.ToObject(typeof(ShipDirection), direction);
        }
    }
}