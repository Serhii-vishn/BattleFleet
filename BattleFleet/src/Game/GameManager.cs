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

        private void setupPlayers()
        {
            Console.Write("\n\n\t\t\tEnter Player 1 name: ");
            string player1Name = Console.ReadLine();

            if (string.IsNullOrEmpty(player1Name))
                player1Name = "player1";

            player1.SetPlayerName(player1Name);

            Console.Write("\n\t\t\tEnter Player 2 name: ");
            string player2Name = Console.ReadLine();

            if (string.IsNullOrEmpty(player2Name))
                player2Name = "player2";

            player2.SetPlayerName(player2Name);
        }

        public Game InitializeGame()
        {
            setupPlayers();
            Console.Clear();

            Console.WriteLine("The sea battle game has started!");
            Console.WriteLine($"Player 1: {player1.GetPlayerName()}");
            Console.WriteLine($"Player 2: {player2.GetPlayerName()}");

            HumanPlayer human1 = new HumanPlayer(player1.GetPlayerName());
            HumanPlayer human2 = new HumanPlayer(player2.GetPlayerName());
            Game game = new Game(human1, human2);

            return game;
        }

        public void DisplayGameStatistics(Game game)
        {
            //
        }
    }
}