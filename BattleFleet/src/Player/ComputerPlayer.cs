using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class ComputerPlayer : Player
    {
        public ComputerPlayer() : base() { }

        public ComputerPlayer(string playerName)
        {
            this.playerName = playerName;
        }

        //public override void ClearBoard()
        //{
        //    shipPlacement.Clear();
        //    ownBoard.Clear();
        //}

        public override void DrawBoard()
        {
            Console.WriteLine($"Player's board: {playerName}");
            Console.WriteLine(this.ownBoard.Draw());
        }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override bool MakeMove()
        {
            return false;
        }

        public override void PlaceShips(PlacementMode placementMode)
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
                        Console.Clear();
                    }
                }
            }
        }
    }
}