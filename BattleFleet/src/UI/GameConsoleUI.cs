using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }



    }
}
