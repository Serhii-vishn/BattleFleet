using System;

namespace BattleFleet.src.UI
{
    class GameConsoleUI
    {
        public int consoleWidth {  get; private set; }
        public int consoleHeight { get; private set; }

        public GameConsoleUI()
        {
            Console.Title = "BattleFleet";

            consoleWidth = 100;
            consoleHeight = 50;

            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
        }

        public void DisplayHeader()
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
        }
       

        public void DisplayMainMenu()
        {
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("\t\t\tSelect the menu item:");
            Console.WriteLine("\t\t\t\t1. Start a new game");
            Console.WriteLine("\t\t\t\t2. Watch previous matches");
            Console.WriteLine("\t\t\t\t3. Read the rules of the game");
            Console.WriteLine("\t\t\t\t0. Exit");
            Console.Write("\t\t\tOption: ");
        }
    }
}