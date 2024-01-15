namespace BattleFleet.src.Game
{
    using BattleFleet.src.Player;
    class GameManager
    {
        private readonly Player player1;
        private readonly Player player2;

        public GameManager(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            setupPlayers();
        }
        public Game InitializeGame()
        {
            Console.Clear();

            Console.WriteLine("\n\n\t\t\t\tThe sea battle game has started!");
            Console.WriteLine($"\t\t\t\tPlayer 1: {player1.GetPlayerName()}");
            Console.WriteLine($"\t\t\t\tPlayer 2: {player2.GetPlayerName()}");
            Console.ReadLine();

            if (player2.GetType() == typeof(ComputerPlayer))
            {
                HumanPlayer human1 = new HumanPlayer(player1.GetPlayerName());
                ComputerPlayer computerPlayer = new ComputerPlayer(player2.GetPlayerName());

                Game game = new Game(human1, computerPlayer);

                return game;
            }
            else
            {
                HumanPlayer human1 = new HumanPlayer(player1.GetPlayerName());
                HumanPlayer human2 = new HumanPlayer(player2.GetPlayerName());
                Game game = new Game(human1, human2);

                return game;
            }
        }

        private void setupPlayers()
        {
            Console.Write("\n\n\t\t\tEnter Player 1 name: ");
            string player1Name = Console.ReadLine();

            if (string.IsNullOrEmpty(player1Name))
                player1Name = "player1";

            player1.SetPlayerName(player1Name);

            if(player2.GetType() == typeof(ComputerPlayer))
            {
                player2.SetPlayerName("Strategist (computer)");
            }
            else
            {
                Console.Write("\n\t\t\tEnter Player 2 name: ");
                string player2Name = Console.ReadLine();

                if (string.IsNullOrEmpty(player2Name))
                    player2Name = "player2";

                player2.SetPlayerName(player2Name);
            }
        }
    }
}