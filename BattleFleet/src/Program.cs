using BattleFleet.src.Player;
using BattleFleet.src.PlayerBoard;
using BattleFleet.src.UI;
using System.Data.Common;

internal class Program
{
    public static void Main()
    {
        GameConsoleUI gameConsoleUI = new GameConsoleUI();
        gameConsoleUI.DisplayHeader();

        bool keyMainMenu = true;

        while (keyMainMenu)
        {
            gameConsoleUI.DisplayMainMenu();
            char option = Console.ReadKey().KeyChar;

            switch (option)
            {
                case '1':
                    {
                        Console.Clear();
                        Board b1 = new Board();
                        Board b2 = new Board();

                        HumanPlayer humanPlayer = new HumanPlayer("pl1");
                        humanPlayer.Initialize(b1, b2);
                        break;
                    }
                case '2':
                    {
                        break;
                    }
                case '3':
                    {
                        gameConsoleUI.DisplayRules();
                        break;
                    }
                case '0':
                    {
                        gameConsoleUI.ExitGame();
                        keyMainMenu = false;
                        break;
                    }
                default:
                    {
                        Console.Write("\n\t\t\tInvalid option, try again...");
                        break;
                    }
            }
        }
        
        Console.ReadKey();
    }
}