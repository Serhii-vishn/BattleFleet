using BattleFleet.src.PlayerBoard;
using BattleFleet.src.UI;

internal class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        GameConsoleUI gameConsoleUI = new GameConsoleUI();
        gameConsoleUI.DisplayHeader();

        gameConsoleUI.DisplayMainMenu();

        Console.ReadKey();
    }
}