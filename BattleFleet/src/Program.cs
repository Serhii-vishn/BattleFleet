using BattleFleet.src.Player;
using BattleFleet.src.PlayerBoard;
using BattleFleet.src.UI;

internal class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //GameConsoleUI gameConsoleUI = new GameConsoleUI();
        //gameConsoleUI.DisplayHeader();

        //gameConsoleUI.DisplayMainMenu();

        Console.Clear();
        Board b1 = new Board();
        Board b2 = new Board();

        HumanPlayer humanPlayer = new HumanPlayer("pl1");
        humanPlayer.Initialize(b1, b2);


        do
        {
            Console.Clear();
            humanPlayer.DrawBoard();

            try
            {
                humanPlayer.PlaceShips();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.Write("Do you want to place another ship? (Y/N): ");

        } while (Console.ReadKey().Key == ConsoleKey.Y);


        Console.ReadKey();

        // humanPlayer.MakeMove();
        Console.ReadKey();
    }
}