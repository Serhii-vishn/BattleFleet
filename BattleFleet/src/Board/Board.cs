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
            board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(i => $" {alphabetCells[i]} |")));

            for (int i = 0; i < kGridLength; i++)
            {
                board.AppendLine(horizontalSeparator);
                board.Append($" {i} |");

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

        public bool placeShip(int row, char column, ShipClass shipClass, ShipDirection shipDirection)
        {
            try
            {
                int columnIndex = verifyPosition(row, column);
                CanPlaceShip(row, columnIndex, shipClass, shipDirection);

                switch (shipDirection)
                {
                    case ShipDirection.VERTICAL:
                        {
                            for (int size = 1, i = row; size <= ((int)shipClass); i++, size++)
                            {
                                grid[i, columnIndex].UpdateCellStatus(CellStatus.OCCUPIED);
                            }
                            break;
                        }
                    case ShipDirection.HORIZONTAL:
                        {
                            for (int size = 1, i = columnIndex; size <= ((int)shipClass); i++, size++)
                            {
                                grid[row, i].UpdateCellStatus(CellStatus.OCCUPIED);
                            }
                            break;
                        }                      
                    default:
                        throw new ArgumentException("Invalid value for a Direction ship.");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private void CanPlaceShip(int row, int column, ShipClass shipClass, ShipDirection shipDirection)
        {
            switch (shipDirection)
            {
                case ShipDirection.VERTICAL:
                    {
                        for(int size = 1; size <= ((int)shipClass); row++, size++)
                        {                          
                            if (grid[row, column].GetCellStatus() != CellStatus.EMPTY)
                            {
                                if (grid[row, column].GetCellStatus() == CellStatus.OCCUPIED)
                                    throw new ArgumentException("Сan't put a ship in its way, there is another ship");
                                else if (grid[row, column].GetCellStatus() == CellStatus.FORBIDDEN)
                                    throw new ArgumentException("Сan't place a ship, it occupies the free zone of another ship");
                            }                               
                        }
                        break;
                    }
                case ShipDirection.HORIZONTAL:
                    {
                        for (int size = 1; size <= ((int)shipClass); column++, size++)
                        {
                            if (grid[row, column].GetCellStatus() == CellStatus.OCCUPIED)
                                throw new ArgumentException("Сan't put a ship in its way, there is another ship");
                            else if (grid[row, column].GetCellStatus() == CellStatus.FORBIDDEN)
                                throw new ArgumentException("Сan't place a ship, it occupies the free zone of another ship");
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid value for a Direction ship.");
            }
        }
    }
}
