using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleFleet.src.Board
{
    class Board
    {
        private const int kGridLength = 10;
        private Cell[,] grid;
        private List<Ship> shipsList;
        private char[] alphabetCells = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        public Board()
        {
            grid = new Cell[10, 10];
            shipsList = new List<Ship>();

            InitializeBoard();
            InitializeShips();
        }

        private void InitializeBoard()
        {
            for(int i = 0; i < kGridLength; i++)
            {
                for (int j = 0; j < kGridLength; j++)
                {
                    grid[i, j] = new Cell(i, alphabetCells[j]);
                }
            }
        }

        private void InitializeShips()
        {
            shipsList.Add(new Ship(ShipClass.FIVE_DECK));

            shipsList.Add(new Ship(ShipClass.THREE_DECK));
            shipsList.Add(new Ship(ShipClass.THREE_DECK));

            shipsList.Add(new Ship(ShipClass.TWO_DECK));
            shipsList.Add(new Ship(ShipClass.TWO_DECK));
            shipsList.Add(new Ship(ShipClass.TWO_DECK));

            shipsList.Add(new Ship(ShipClass.ONE_DECK));
            shipsList.Add(new Ship(ShipClass.ONE_DECK));
            shipsList.Add(new Ship(ShipClass.ONE_DECK));
            shipsList.Add(new Ship(ShipClass.ONE_DECK));
        }

        public string DrawBoard()
        {
            const string horizontalSeparator = "\n---+---+---+---+---+---+---+---+---+---+---+";
            StringBuilder board = new StringBuilder();

            board.Append(" X |");
            board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(i => $" {i} |")));

            for (int i = 0; i < kGridLength; i++)
            {
                board.AppendLine(horizontalSeparator);
                board.Append($" {alphabetCells[i]} |");

                board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(j => grid[i, j].GetCellCode())));
            }

            return board.ToString();
        }
    
        public bool checkMove(int row, char column)
        {
            try
            {
                if (row < 0 || row >= kGridLength)
                    throw new ArgumentOutOfRangeException(nameof(row), "Incorrect value for a row.");

                int columnIndex = validateColumn(column);
                if (columnIndex == -1)
                    throw new ArgumentException("Invalid value for a column.", nameof(column));

                CellStatus state = grid[row, columnIndex].GetCellStatus();
                return state == CellStatus.EMPTY;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public int validateColumn(char column)
        {
            for (int i = 0; i < kGridLength; i++)
            {
                if (alphabetCells[i] == char.ToUpper(column))
                    return i;
            }
            return -1;
        }
    }
}
