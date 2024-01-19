namespace BattleFleet.src.Game
{
    using BattleFleet.src.Player;
    class GameManager
    {
        private readonly Player _player1;
        private readonly Player _player2;

        public GameManager(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;

            SetupPlayers();
        }
        public Game InitializeGame()
        {
            Console.Clear();

            Console.WriteLine("\n\n\t\t\t\tThe sea battle game has started!");
            Console.WriteLine($"\t\t\t\tPlayer 1: {_player1.GetPlayerName()}");
            Console.WriteLine($"\t\t\t\tPlayer 2: {_player2.GetPlayerName()}");
            Console.ReadLine();

            if (_player2.GetType() == typeof(ComputerPlayer))
            {
                HumanPlayer human1 = new(_player1.GetPlayerName());
                ComputerPlayer computerPlayer = new(_player2.GetPlayerName());

                Game game = new(human1, computerPlayer);

                return game;
            }
            else
            {
                HumanPlayer human1 = new(_player1.GetPlayerName());
                HumanPlayer human2 = new(_player2.GetPlayerName());
                Game game = new(human1, human2);

                return game;
            }
        }

        private void SetupPlayers()
        {
            Console.Write("\n\n\t\t\tEnter Player 1 name: ");
            string player1Name = Console.ReadLine();

            if (string.IsNullOrEmpty(player1Name))
                player1Name = "player1";

            _player1.SetPlayerName(player1Name);

            if(_player2.GetType() == typeof(ComputerPlayer))
            {
                _player2.SetPlayerName("Strategist (computer)");
            }
            else
            {
                Console.Write("\n\t\t\tEnter Player 2 name: ");
                string player2Name = Console.ReadLine();

                if (string.IsNullOrEmpty(player2Name))
                    player2Name = "player2";

                _player2.SetPlayerName(player2Name);
            }
        }
    }
}