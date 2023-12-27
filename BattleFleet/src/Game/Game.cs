using System;

namespace BattleFleet.src.Game
{
    using BattleFleet.src.PlayerBoard;
    using BattleFleet.src.Player;

    class Game : IGameRules
    {
        private Board player1Board;
        private Board player2Board;

        private HumanPlayer player1;
        private HumanPlayer player2;

        private Player currentPlayer;

        public Game(HumanPlayer player1, HumanPlayer player2)
        {
            player1Board = new Board();
            player2Board = new Board();

            this.player1 = player1;
            this.player2 = player2;

            this.player1.Initialize(player1Board, player2Board);
            this.player2.Initialize(player2Board, player1Board);

            currentPlayer = this.player1;
        }

        private void addShipsOnBoard()
        {
            Console.WriteLine("Game start. Now you have to place the ships on the field");
            do
            {
                Console.Clear();
                currentPlayer.DrawBoard();

                try
                {
                    currentPlayer.PlaceShips();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
                if(currentPlayer.CountAvaliableShips()==0)
                {
                    Console.WriteLine("All ships have been used. Let's get to the game");
                    Console.ReadKey();
                    break;
                }
                Console.Write($"There are {currentPlayer.CountAvaliableShips()} more ships available. " +
                                "\nEnter 'N' for an incomplete ship composition: ");

            } while (Console.ReadKey().Key != ConsoleKey.N);

            Console.Clear();
            Console.Write ("Final. ");
            currentPlayer.DrawBoard();

            Console.ReadKey();
        }

        private bool isGameOver()
        {
            if(player1Board.GetAliveShipsCount() > 0 || player2Board.GetAliveShipsCount() > 0)
                return false;
            else
                return true;
        }

        private void startBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle start. Now you have to shoot on the field");

            while (!isGameOver())
            {
                currentPlayer.DrawBoard();
                try
                {
                    bool successfulShot = currentPlayer.MakeMove();
                    if (!successfulShot)
                    {
                        Console.WriteLine("Missed!");
                        SwitchTurn();
                    }
                    else
                    {
                        Console.WriteLine("Nice shoot!");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void StartGame()
        {
            addShipsOnBoard();

            SwitchTurn();
            addShipsOnBoard();

            startBattle();
        }

        public void SwitchTurn()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        public void EndGame()
        {

        }
    }
}