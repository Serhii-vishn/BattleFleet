namespace BattleFleet.src.Game
{
    using BattleFleet.src.PlayerBoard;
    using BattleFleet.src.Player;
    using BattleFleet.src.UI;
    using BattleFleet.src.FileManager;

    class Game : IGameRules
    {
        private Board player1Board;
        private Board player2Board;

        private HumanPlayer player1;
        private HumanPlayer player2;

        private Player currentPlayer;

        private ShipTemplateManager templateManager;

        public Game(HumanPlayer player1, HumanPlayer player2)
        {
            player1Board = new Board();
            player2Board = new Board();

            this.player1 = player1;
            this.player2 = player2;

            this.player1.Initialize(player1Board, player2Board);
            this.player2.Initialize(player2Board, player1Board);

            currentPlayer = this.player1;

            this.templateManager = new ShipTemplateManager();
        }

        private void placeShipsPhase()
        {
            bool keyMenu = true;

            do
            {
                GameConsoleUI.DisplayPlaceShipsMenu(currentPlayer.GetPlayerName());
                char option = Console.ReadKey().KeyChar;

                switch (option)
                {
                    case '1':
                        {
                            templatesPlacementShips();
                            keyMenu = false;
                            break;
                        }
                    case '2':
                        {
                            randomPlacementShips();
                            keyMenu = false;
                            break;
                        }
                    case '3':
                        {
                            manualPlacementShips();
                            keyMenu = false;
                            break;
                        }
                    default:
                        {
                            Console.Write("\n\t\t\tInvalid option, try again...");
                            break;
                        }
                }
            } while (keyMenu);
        }

        private void saveTemlateFile()
        {
            Console.Write("\n\tEnter template name: ");
            string tplName = Console.ReadLine();
            var template = currentPlayer.GetShipPlacement();

            templateManager.SaveTemplate(template, tplName);
        }

        private void templatesPlacementShips()
        {
            do
            {
                Console.Clear();
                currentPlayer.ClearBoard();

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
                {
                    Console.WriteLine("\t\tInvalid data. Please enter a valid template number.");
                    Console.ReadKey();
                    continue;
                }

                var template = templateManager.GetTemplateByName(templNames[templNumber - 1]);

                currentPlayer.PlaceShipsTemplate(template);

                currentPlayer.DrawBoard();

                Console.Write("\nUse this template (Y/N): ");
            } while (Console.ReadKey().Key != ConsoleKey.Y);
            Console.ReadKey();
        }

        private void randomPlacementShips()
        {
            do
            {
                Console.Clear();
                currentPlayer.ClearBoard();

                currentPlayer.PlaceShipsRandom();

                currentPlayer.DrawBoard();

                Console.Write("\n\tUse this(Y/N): ");
            } while (Console.ReadKey().Key != ConsoleKey.Y);

            Console.Write("\n\tWant to save this template(Y/N): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
                saveTemlateFile();

            Console.ReadKey();
        }

        private void manualPlacementShips()
        {
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

                if (currentPlayer.CountAvaliableShips() == 0)
                {
                    Console.WriteLine("All ships have been used. Let's get to the game");
                    Console.ReadKey();
                    break;
                }
                Console.Write($"There are {currentPlayer.CountAvaliableShips()} more ships available. " +
                                "\nEnter 'N' for an incomplete ship composition: ");

            } while (Console.ReadKey().Key != ConsoleKey.N);

            Console.Clear();

            Console.Write("Final. ");
            currentPlayer.DrawBoard();

            Console.Write("\n\tWant to save this template(Y/N): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
                saveTemlateFile();

            Console.ReadKey();
        }

        private bool isGameOver()
        {
            if (player1Board.GetAliveShipsCount() > 0 || player2Board.GetAliveShipsCount() > 0)
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
                bool correctShot;
                do
                {
                    currentPlayer.DrawBoard();
                    correctShot = currentPlayer.MakeMove();

                    Console.ReadKey();
                    Console.Clear();
                } while (correctShot);

                SwitchTurn();

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void StartGame()
        {
            placeShipsPhase();

            SwitchTurn();

            placeShipsPhase();

            startBattle();
        }

        public void SwitchTurn()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        public void EndGame()
        {
            Console.WriteLine("Game end");
        }
    }
}