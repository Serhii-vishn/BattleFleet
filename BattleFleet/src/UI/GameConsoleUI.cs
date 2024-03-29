﻿namespace BattleFleet.src.UI
{
    public static class GameConsoleUI
    {
        private static int consoleWidth { get; set; }
        private static int consoleHeight { get; set; }

        public static void Initialize()
        {
            Console.Title = "BattleFleet";

            consoleWidth = 100;
            consoleHeight = 50;

            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
        }

        public static void DisplayHeader()
        {
            Console.Clear();

            string[] initialMessages = {
                "           ~```",
                "       ||",
                "  /=========\\",
                "_\\__\\\\____|_________|__//_//_",
                "\\                           /",
                " \\    o   o   o   o   o    /",
                " \\                       /",
                "~~~|~~~~~~~~~~~~~~~~~~~~~|~~~",
                " ",
                "Welcome to the game - BattleFleet",
                "\t  *By @Serhii-vishn",
                "\t      **Version 1.1"
            };

            int centerX = consoleWidth / 2;
            int centerY = (consoleHeight - initialMessages.Length) / 2;

            foreach (string message in initialMessages)
            {
                Console.SetCursorPosition(centerX - message.Length / 2, centerY++);
                Console.WriteLine(message);
            }
            Console.ReadKey();
        }


        public static void DisplayMainMenu()
        {
            Console.Clear();

            Console.WriteLine("\n\n\n\t\t\tSelect the menu item:");
            Console.WriteLine("\t\t\t\t1. Start PvP game");
            Console.WriteLine("\t\t\t\t2. Start game against a computer");
            Console.WriteLine("\t\t\t\t3. Watch previous matches");
            Console.WriteLine("\t\t\t\t4. Read the rules of the game");
            Console.WriteLine("\t\t\t\t0. Exit");
            Console.Write("\t\t\tOption: ");
        }

        public static void DisplayRules()
        {
            Console.Clear();

            Console.WriteLine("\n\n\n\t\t\t\t    Rules of the game 'BattleFleet':");
            Console.WriteLine("\t\t-------------------------------------------------------------------------");
            Console.WriteLine("\t\t1. Players place their ships on the playing field");
            Console.WriteLine("\t\t2. Each player has their own playing field and the opponent's field");
            Console.WriteLine("\t\t3. Players take turns choosing cells to fire, trying to sink enemy ships");
            Console.WriteLine("\t\t4. The game continues until all the ships of one of the players are sunk");
            Console.WriteLine("\t\t5. The player who is the first to sink all the enemy ships wins");
            Console.WriteLine("\t\t-------------------------------------------------------------------------");
            Console.Write("\t\tPress any key to return to the main menu...");

            Console.ReadKey();
        }

        public static void ExitGame()
        {
            Console.Clear();

            Console.WriteLine("\n\n\n\t\t\t\t\tThank you for playing!");
            Console.WriteLine("\t\t\t\tWe wish you a good day and good mood!");

            Console.WriteLine("\t\t\t\tThe game is closed. See you soon!");
            Environment.Exit(0);
        }

        public static void DisplayPlaceShipsMenu(string playerName)
        {
            Console.Clear();
            Console.Write($"\n\t\t\t\t\tPlayer: {playerName} " +
                        "\n\t\t\tGame start. Now you have to place the ships on the field" +
                        "\n\t\t\t\t1. Use ready-made templates" +
                        "\n\t\t\t\t2. Randomize ship placement" +
                        "\n\t\t\t\t3. Create your own ship distribution" +
                        "\n\t\t\tOption: ");
        }
    }
}