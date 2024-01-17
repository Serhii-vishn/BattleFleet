﻿using BattleFleet.src.PlayerBoard;

namespace BattleFleet.src.Player
{
    public class ComputerPlayer : Player
    {
        private Dictionary<char, int> succesShots;
        
        public ComputerPlayer() : base() 
        { 
            succesShots = new Dictionary<char, int>(); 
        }

        public ComputerPlayer(string playerName)
        {
            succesShots = new Dictionary<char, int>();
            this.playerName = playerName;
        }

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
            Random random = new Random();

            bool isSuccses = false;

            char randomColumn;
            int randomRow;

            do
            {
                if (succesShots.Count > 0)
                {
                    var lastShot = succesShots.Last();

                    randomColumn = lastShot.Key;
                    randomRow = lastShot.Value;

                    int direction = random.Next(0, 4);

                    switch (direction)
                    {
                        case 0:
                            randomRow--;
                            break;
                        case 1:
                            randomRow++;
                            break;
                        case 2:
                            randomColumn--;
                            break;
                        case 3:
                            randomColumn++;
                            break;
                    }
                }
                else
                {
                    randomColumn = (char)random.Next(65, 75);
                    randomRow = random.Next(0, 10);
                }

                try
                {
                    if (opponentBoard.MoveCheck(randomRow, randomColumn))
                    {
                        isSuccses = opponentBoard.MoveShoot(randomRow, randomColumn);

                        if (isSuccses)
                            succesShots.Add(randomColumn, randomRow);
                    }
                }
                catch
                {
                    isSuccses = true;
                    succesShots.Clear();
                }
            } while (isSuccses);
        
            return isSuccses;
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