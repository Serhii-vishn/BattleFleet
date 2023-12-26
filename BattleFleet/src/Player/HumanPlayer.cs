using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class HumanPlayer : Player
    {
        public HumanPlayer() : base() { }

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


            Console.WriteLine($"Ship size in cells:(1){ShipClass.FIVE_DECK}" +
                                $",(2){ShipClass.THREE_DECK}" +
                                    $",(3){ShipClass.TWO_DECK}" +
                                        $",(4){ShipClass.ONE_DECK}");

            Console.Write("Ship class: ");
            int shipSize;
            if (!int.TryParse(Console.ReadLine(), out shipSize) ||
                !Enum.IsDefined(typeof(ShipClass), shipSize))
            {
                throw new ArgumentException("Invalid ship size. Please enter a valid ship size.");
            }
            ShipClass shipClass = (ShipClass)Enum.ToObject(typeof(ShipClass), shipSize);

            Console.Write($"Ship direction {ShipDirection.HORIZONTAL} - 0, {ShipDirection.VERTICAL} - 1: ");
            int direction;
            if (!int.TryParse(Console.ReadLine(), out direction) ||
                !Enum.IsDefined(typeof(ShipDirection), direction))
            {
                throw new ArgumentException("Invalid ship direction. Please enter a valid direction.");
            }
            ShipDirection shipDirection = (ShipDirection)Enum.ToObject(typeof(ShipDirection), direction);

            ownBoard.MovePlaceShip(row, column, shipClass, shipDirection);
        }

        public override void MakeMove()
        {
            try
            {
                Console.WriteLine($"Player's move: {playerName}.");

                Console.Write("Enter: column (A-J): ");
                char column = Console.ReadKey().KeyChar;

                Console.Write(" row (0-9): ");
                int row = int.Parse(Console.ReadLine());              

                if (opponentBoard.CheckMove(row, column))
                {
                    if(opponentBoard.MoveShoot(row, column))
                    {
                        Console.WriteLine("Nice shoot!");
                    }
                    else
                    {
                        Console.WriteLine("Naaah miss");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}