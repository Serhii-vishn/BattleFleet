
using System;
using System.Data.Common;
using System.Text;

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

            initializeBoard();
        }

        private void initializeBoard()
        {
            for(int i = 0; i < kGridLength; i++)
            {
                for (int j = 0; j < kGridLength; j++)
                {
                    grid[i, j] = new Cell(i, alphabetCells[j]);
                }
            }
        }    

        private int verifyPosition(int row, char column)
        {
            if (row < 0 || row >= kGridLength)
                throw new ArgumentOutOfRangeException("Incorrect value for a row.", nameof(row));

            int columnIndex = -1;
            for (int i = 0; i < kGridLength; i++)
            {
                if (alphabetCells[i] == char.ToUpper(column))
                    columnIndex = i;
            }
            if (columnIndex == -1)
                throw new ArgumentException("Incorrect value for a column.", nameof(column));

            return columnIndex;
        }      

        private void canCreateNewShip(ShipClass shipClass)
        {
            int numberShips = 0;

            foreach (Ship ship in shipsList)
            {
                if (ship.GetShipClass() == shipClass)
                {
                    numberShips++;
                }
            }

            switch (shipClass)
            {
                case ShipClass.ONE_DECK:
                    {
                        if (numberShips >= 4)
                            throw new InvalidOperationException("Cannot place more than 4 ships of ONE_DECK class.");

                        break;
                    }
                case ShipClass.TWO_DECK:
                    {
                        if (numberShips >= 3)
                            throw new InvalidOperationException("Cannot place more than 3 ships of TWO_DECK class.");

                        break;
                    }
                case ShipClass.THREE_DECK:
                    {
                        if (numberShips >= 2)
                            throw new InvalidOperationException("Cannot place more than 2 ships of THREE_DECK class.");

                        break;
                    }
                case ShipClass.FIVE_DECK:
                    {
                        if (numberShips >= 1)
                            throw new InvalidOperationException("Cannot place more than 1 ship of FIVE_DECK class.");

                        break;
                    }
                default:
                    throw new ArgumentException("Invalid ship class");
            }
        }

        private void canPlaceShip(int row, int column, ShipClass shipClass, ShipDirection shipDirection)
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

        private void markShipAreaCells(int row, int column, ShipClass shipClass, ShipDirection shipDirection, CellStatus newCellStatus)
        {
            switch (shipDirection)
            {
                case ShipDirection.HORIZONTAL:
                    {
                        int startRow = Math.Max(row - 1, 0);
                        int endRow = Math.Min(row + 1, kGridLength - 1);
                        int startColumn = Math.Max(column - 1, 0);
                        int endColumn = Math.Min(column + (int)shipClass, kGridLength - 1);

                        for (int i = startRow; i <= endRow; i++)
                        {
                            for (int j = startColumn; j <= endColumn; j++)
                            {
                                grid[i, j].UpdateCellStatus(newCellStatus);
                            }
                        }
                        break;
                    }
                case ShipDirection.VERTICAL:
                    {
                        int startRow = Math.Max(row - 1, 0);
                        int endRow = Math.Min(row + (int)shipClass, kGridLength - 1);
                        int startColumn = Math.Max(column - 1, 0);
                        int endColumn = Math.Min(column + 1, kGridLength - 1);

                        for (int i = startRow; i <= endRow; i++)
                        {
                            for (int j = startColumn; j <= endColumn; j++)
                            {
                                grid[i, j].UpdateCellStatus(newCellStatus);
                            }
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid value for a Direction ship.");
            }
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

        public bool MovePlaceShip(int row, char column, ShipClass shipClass, ShipDirection shipDirection)
        {
            try
            {
                canCreateNewShip(shipClass);

                int columnIndex = verifyPosition(row, column);
                canPlaceShip(row, columnIndex, shipClass, shipDirection);

                markShipAreaCells(row, columnIndex, shipClass, shipDirection, CellStatus.FORBIDDEN);

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
                shipsList.Add(new Ship(shipClass, new Dictionary<char, int> { { column, row } }, shipDirection));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool CheckMove(int row, char column)
        {
            try
            {
                int columnIndex = verifyPosition(row, column);

                CellStatus state = grid[row, columnIndex].GetCellStatus();
                if (state == CellStatus.HIT)
                    throw new Exception("Cannot be re-fired");
                return state != CellStatus.HIT;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool MoveShoot(int row, char column)
        {
            try
            {
                int columnIndex = verifyPosition(row, column);

                CellStatus state = grid[row, columnIndex].GetCellStatus();
                switch (state)
                {
                    case CellStatus.OCCUPIED:
                        {
                            grid[row, columnIndex].UpdateCellStatus(CellStatus.HIT);

                            shipsList
                                .Where(ship => ship.GetPosition().TryGetValue(column, out int shipRow) && shipRow == row)
                                .ToList()
                                .ForEach(ship =>
                                {
                                    ship.Hit();
                                    if (ship.GetHealth() == 0)
                                    {
                                        markShipAreaCells(row, column, ship.GetShipClass(), ship.GetDirection(), CellStatus.MISS);
                                    }
                                });

                            return true;
                        }
                    case CellStatus.EMPTY:
                        grid[row, columnIndex].UpdateCellStatus(CellStatus.MISS);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }
    }
}