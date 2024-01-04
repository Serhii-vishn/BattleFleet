using BattleFleet.src.Game;
using BattleFleet.src.Player;
using BattleFleet.src.UI;

internal class Program
{
    public static void Main()
    {
        GameConsoleUI gameConsoleUI = new GameConsoleUI();
        gameConsoleUI.DisplayHeader();

        bool keyMainMenu = true;

        do
        {
            gameConsoleUI.DisplayMainMenu();
            char option = Console.ReadKey().KeyChar;

            switch (option)
            {
                case '1':
                    {
                        Console.Clear();
                        HumanPlayer player1 = new HumanPlayer();
                        HumanPlayer player2 = new HumanPlayer();

                        GameManager gameManager = new GameManager(player1, player2);
                        Game game = gameManager.InitializeGame();

                        game.StartGame();

                        game.EndGame();

                        gameConsoleUI.ExitGame();
                        keyMainMenu = false;
                        break;
                    }
                case '2':
                    {
                        Console.WriteLine("\nin development...");
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine("\nin development...");
                        break;
                    }
                case '4':
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
                        //Console.Write("\n\t\t\tInvalid option, try again...");
                        break;
                    }
            }
        } while (keyMainMenu);

        Console.ReadKey();
    }
}