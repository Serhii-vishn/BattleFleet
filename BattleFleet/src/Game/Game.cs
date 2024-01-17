namespace BattleFleet.src.Game
{
    using BattleFleet.src.PlayerBoard;
    using BattleFleet.src.Player;
    using BattleFleet.src.UI;
    using BattleFleet.src.FileManager;

    class Game : IGameRules
    {
        private readonly Board player1Board;
        private readonly Board player2Board;

        private readonly HumanPlayer player1;
        private readonly HumanPlayer player2;

        private readonly ComputerPlayer computerPlayer;

        private Player currentPlayer;

        private readonly ShipTemplateManager templateManager;

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

        public Game(HumanPlayer player1, ComputerPlayer computerPlayer)
        {
            player1Board = new Board();
            player2Board = new Board();

            this.player1 = player1;
            this.computerPlayer = computerPlayer;

            this.player1.Initialize(player1Board, player2Board);
            this.computerPlayer.Initialize(player2Board, player1Board);

            currentPlayer = this.computerPlayer;

            this.templateManager = new ShipTemplateManager();
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
            if (currentPlayer == player1 && computerPlayer is null)
                currentPlayer = player2;
            else if (currentPlayer == player2 && computerPlayer is null)
                currentPlayer = player1;
            else if (currentPlayer == player1 && player2 is null)
                currentPlayer = computerPlayer;
            else if (currentPlayer == computerPlayer)
                currentPlayer = player1;
        }

        public void EndGame()
        {
            currentPlayer = getWinner();
            Console.WriteLine($"\t\t\tGame end. Winner: {currentPlayer.GetPlayerName()}" +
                                "\n\t\t\tPlayers Boards:");

            player1.DrawBoard();
            player2.DrawBoard();
            Console.ReadKey();
        }

        private void placeShipsPhase()
        {
            if (currentPlayer == computerPlayer)
            {
                currentPlayer.PlaceShips(PlacementMode.RANDOM);
            }
            else
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
        }

        private bool isNameExist(string name)
        {
            var tmplNames = templateManager.GetTemplateNames();
            foreach (var tmpl in tmplNames)
            {
                if (tmpl == name)
                    return false;
            }
            return true;
        }

        private void saveTemlateFile()
        {
            string templateName;
            do
            {
                Console.Write("\r\n\tEnter template name: ");
                templateName = Console.ReadLine();

                if (!isNameExist(templateName))
                    Console.WriteLine("This name already exists, enter another one");

            } while (!isNameExist(templateName));

            var template = currentPlayer.GetShipPlacement();
            templateManager.SaveTemplate(template, templateName);
        }

        private void templatesPlacementShips()
        {
            do
            {
                Console.Clear();
      
                try
                {
                    currentPlayer.ClearBoard();

                    currentPlayer.PlaceShips(PlacementMode.TEMPLATE);

                    currentPlayer.DrawBoard();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                    continue;
                }

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

                currentPlayer.PlaceShips(PlacementMode.RANDOM);

                Console.Clear();
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
                    currentPlayer.PlaceShips(PlacementMode.MANUAL);
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

        private Player getWinner()
        {
            if (computerPlayer is null)
                return player1Board.GetAliveShipsCount() > 0 ? player1 : player2;
            else
                return player1Board.GetAliveShipsCount() > 0 ? player1 : computerPlayer;
        }

        private bool isGameOver()
        {
            if (player1Board.GetAliveShipsCount() > 0 && player2Board.GetAliveShipsCount() > 0)
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
                    if (isGameOver())
                        break;

                    if (currentPlayer != computerPlayer)
                        currentPlayer.DrawBoard();

                    correctShot = currentPlayer.MakeMove();

                    if (currentPlayer != computerPlayer)
                    {
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (correctShot);

                SwitchTurn();

                if (computerPlayer is null)
                {
                    Console.ReadKey();
                    Console.Clear();
                }                
            }
        }
    }
}