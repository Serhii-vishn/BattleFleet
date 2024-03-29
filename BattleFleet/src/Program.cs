﻿using BattleFleet.src.Game;
using BattleFleet.src.Player;
using BattleFleet.src.UI;

namespace BattleFleet
{
    internal class Program
    {
        public static void Main()
        {
            GameConsoleUI.Initialize();
            GameConsoleUI.DisplayHeader();

            bool keyMainMenu = true;

            do
            {
                GameConsoleUI.DisplayMainMenu();
                char option = Console.ReadKey().KeyChar;

                switch (option)
                {
                    case '1':
                        {
                            Console.Clear();
                            HumanPlayer player1 = new();
                            HumanPlayer player2 = new();

                            GameManager gameManager = new(player1, player2);
                            Game game = gameManager.InitializeGame();

                            game.StartGame();

                            game.EndGame();

                            GameConsoleUI.ExitGame();
                            keyMainMenu = false;
                            break;
                        }
                    case '2':
                        {
                            Console.Clear();

                            HumanPlayer humanPlayer = new();
                            ComputerPlayer computerPlayer = new();

                            GameManager gameManager = new(humanPlayer, computerPlayer);

                            Game game = gameManager.InitializeGame();

                            game.StartGame();

                            game.EndGame();

                            GameConsoleUI.ExitGame();
                            keyMainMenu = false;
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine("\nin development...");
                            break;
                        }
                    case '4':
                        {
                            GameConsoleUI.DisplayRules();
                            break;
                        }
                    case '0':
                        {
                            GameConsoleUI.ExitGame();
                            keyMainMenu = false;
                            break;
                        }
                }
            } while (keyMainMenu);

            Console.ReadKey();
        }
    }
}