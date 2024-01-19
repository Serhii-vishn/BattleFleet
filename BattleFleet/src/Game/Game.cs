namespace BattleFleet.src.Game
{
    using BattleFleet.src.PlayerBoard;
    using BattleFleet.src.Player;
    using BattleFleet.src.UI;
    using BattleFleet.src.FileManager;

    class Game : IGameRules
    {
        private readonly Board _player1Board;
        private readonly Board _player2Board;

        private readonly HumanPlayer _player1;
        private readonly HumanPlayer _player2;
        private readonly ComputerPlayer _computerPlayer;

        private Player _currentPlayer;

        private readonly ShipTemplateManager _templateManager;

        public Game(HumanPlayer player1, HumanPlayer player2)
        {
            _player1Board = new Board();
            _player2Board = new Board();

            _player1 = player1;
            _player2 = player2;

            _player1.Initialize(_player1Board, _player2Board);
            _player2.Initialize(_player2Board, _player1Board);

            _currentPlayer = _player1;

            _templateManager = new ShipTemplateManager();
        }

        public Game(HumanPlayer player1, ComputerPlayer computerPlayer)
        {
            _player1Board = new Board();
            _player2Board = new Board();

            _player1 = player1;
            _computerPlayer = computerPlayer;

            _player1.Initialize(_player1Board, _player2Board);
            _computerPlayer.Initialize(_player2Board, _player1Board);

            _currentPlayer = _computerPlayer;

            _templateManager = new ShipTemplateManager();
        }

        public void StartGame()
        {
            PlaceShipsPhase();

            SwitchTurn();

            PlaceShipsPhase();

            StartBattle();
        }

        public void SwitchTurn()
        {
            if (_currentPlayer == _player1 && _computerPlayer is null)
                _currentPlayer = _player2;
            else if (_currentPlayer == _player2 && _computerPlayer is null)
                _currentPlayer = _player1;
            else if (_currentPlayer == _player1 && _player2 is null)
                _currentPlayer = _computerPlayer;
            else if (_currentPlayer == _computerPlayer)
                _currentPlayer = _player1;
        }

        public void EndGame()
        {
            _currentPlayer = GetWinner();
            Console.WriteLine($"\t\t\tGame end. Winner: {_currentPlayer.GetPlayerName()}" +
                                "\n\t\t\tPlayers Boards:");

            _player1.DrawBoard();
            _player2.DrawBoard();
            Console.ReadKey();
        }

        private void PlaceShipsPhase()
        {
            if (_currentPlayer == _computerPlayer)
            {
                _currentPlayer.PlaceShips(PlacementMode.RANDOM);
            }
            else
            {
                bool keyMenu = true;

                do
                {
                    GameConsoleUI.DisplayPlaceShipsMenu(_currentPlayer.GetPlayerName());
                    char option = Console.ReadKey().KeyChar;

                    switch (option)
                    {
                        case '1':
                            {
                                TemplatesPlacementShips();
                                keyMenu = false;
                                break;
                            }
                        case '2':
                            {
                                RandomPlacementShips();
                                keyMenu = false;
                                break;
                            }
                        case '3':
                            {
                                ManualPlacementShips();
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

        private bool IsNameExist(string name)
        {
            var tmplNames = _templateManager.GetTemplateNames();
            foreach (var tmpl in tmplNames)
            {
                if (tmpl == name)
                    return false;
            }
            return true;
        }

        private void SaveTemlateFile()
        {
            string templateName;
            do
            {
                Console.Write("\r\n\tEnter template name: ");
                templateName = Console.ReadLine();

                if (!IsNameExist(templateName))
                    Console.WriteLine("This name already exists, enter another one");

            } while (!IsNameExist(templateName));

            var template = _currentPlayer.GetShipPlacement();
            _templateManager.SaveTemplate(template, templateName);
        }

        private void TemplatesPlacementShips()
        {
            do
            {
                Console.Clear();
      
                try
                {
                    _currentPlayer.ClearBoard();

                    _currentPlayer.PlaceShips(PlacementMode.TEMPLATE);

                    _currentPlayer.DrawBoard();
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

        private void RandomPlacementShips()
        {
            do
            {
                Console.Clear();
                _currentPlayer.ClearBoard();

                _currentPlayer.PlaceShips(PlacementMode.RANDOM);

                Console.Clear();
                _currentPlayer.DrawBoard();

                Console.Write("\n\tUse this(Y/N): ");
            } while (Console.ReadKey().Key != ConsoleKey.Y);

            Console.Write("\n\tWant to save this template(Y/N): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
                SaveTemlateFile();

            Console.ReadKey();
        }

        private void ManualPlacementShips()
        {
            do
            {
                Console.Clear();
                _currentPlayer.DrawBoard();

                try
                {
                    _currentPlayer.PlaceShips(PlacementMode.MANUAL);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (_currentPlayer.CountAvaliableShips() == 0)
                {
                    Console.WriteLine("All ships have been used. Let's get to the game");
                    Console.ReadKey();
                    break;
                }
                Console.Write($"There are {_currentPlayer.CountAvaliableShips()} more ships available. " +
                                "\nEnter 'N' for an incomplete ship composition: ");

            } while (Console.ReadKey().Key != ConsoleKey.N);

            Console.Clear();

            Console.Write("Final. ");
            _currentPlayer.DrawBoard();

            Console.Write("\n\tWant to save this template(Y/N): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
                SaveTemlateFile();

            Console.ReadKey();
        }

        private Player GetWinner()
        {
            if (_computerPlayer is null)
                return _player1Board.GetAliveShipsCount() > 0 ? _player1 : _player2;
            else
                return _player1Board.GetAliveShipsCount() > 0 ? _player1 : _computerPlayer;
        }

        private bool IsGameOver()
        {
            if (_player1Board.GetAliveShipsCount() > 0 && _player2Board.GetAliveShipsCount() > 0)
                return false;
            else
                return true;
        }

        private void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle start. Now you have to shoot on the field");

            while (!IsGameOver())
            {
                bool correctShot;
                do
                {                  
                    if (IsGameOver())
                        break;

                    if (_currentPlayer != _computerPlayer)
                        _currentPlayer.DrawBoard();

                    correctShot = _currentPlayer.MakeMove();

                    if (_currentPlayer != _computerPlayer)
                    {
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (correctShot);

                SwitchTurn();

                if (_computerPlayer is null)
                {
                    Console.ReadKey();
                    Console.Clear();
                }                
            }
        }
    }
}