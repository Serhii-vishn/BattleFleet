using BattleFleet.src.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleFleet.src.PlayerBoard
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

                board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(j => grid[i, j].ToString())));
            }

            return board.ToString();
        }
    
        public bool checkMove(int row, char column)
        {
            try
            {               
                int columnIndex = verifyPosition(row, column);

                CellStatus state = grid[row, columnIndex].GetCellStatus();
                return state == CellStatus.EMPTY;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private int verifyPosition(int row, char column)
        {
            if (row < 0 || row >= kGridLength)
                throw new ArgumentOutOfRangeException(nameof(row), "Incorrect value for a row.");

            int columnIndex = -1;
            for (int i = 0; i < kGridLength; i++)
            {
                if (alphabetCells[i] == char.ToUpper(column))
                    columnIndex = i;
            }
            if (columnIndex == -1)
                throw new ArgumentException("Invalid value for a column.", nameof(column));

            return columnIndex;
        }

        public void placeShip(int row, char column, ShipClass shipClass, ShipDirection shipDirection)
        {

           
           //throw new NotImplementedException();
        }

        private bool CanPlaceShip(int row, char column, ShipClass shipClass, ShipDirection shipDirection)
        {

            for (int i = 0, coord = row; i < ((int)shipClass); coord++, i++)
            {
                // if (grid[row, validatecolumn(column)].getcellstatus() != cellstatus.empty)
                //  return false;
            }

            return true;
        }

        private bool CanPlaceShip(char column, int shipSize)
        {
            throw new NotImplementedException();
        }
    }
}
