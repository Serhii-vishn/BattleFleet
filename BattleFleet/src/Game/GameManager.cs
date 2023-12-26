using System;

namespace BattleFleet.src.Game
{
    using BattleFleet.src.Player;
    class GameManager
    {
        private Player player1;
        private Player player2;

        public GameManager(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public Game InitializeGame()
        {
            SetupPlayers();
            Console.WriteLine("The sea battle game has started!");
            Console.WriteLine($"Player 1: {player1.GetPlayerName()}");
            Console.WriteLine($"Player 2: {player2.GetPlayerName()}");

            Game game = new Game(player1, player2);

            return game;
        }

        private void SetupPlayers()
        {
            Console.Write("Enter Player 1 name: ");
            string player1Name = Console.ReadLine();
            player1.SetPlayerName(player1Name);

            Console.Write("Enter Player 2 name: ");
            string player2Name = Console.ReadLine();
            player2.SetPlayerName(player2Name);
        }

        public void DisplayGameStatistics(Game game)
        {
            //
        }
    }
}
