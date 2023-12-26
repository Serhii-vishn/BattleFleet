using System;

namespace BattleFleet.src.Game
{
    using BattleFleet.src.PlayerBoard;
    using BattleFleet.src.Player;

    class Game : IGameRules
    {
        private Board player1Board;
        private Board player2Board;

        private Player player1;
        private Player player2;

        private Player currentPlayer;

        public Game(Player player1, Player player2)
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

                Console.Write("Do you want to place another ship? (Y/N): ");

            } while (Console.ReadKey().Key == ConsoleKey.Y);

            Console.ReadKey();
        }

        private void startBattle()
        {

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