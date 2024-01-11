using System.Text;

namespace BattleFleet.src.PlayerBoard
{
    class Board
    {
        private const int kGridLength = 10;
        private readonly Cell[,] grid;
        private readonly List<Ship> shipsList;
        private readonly char[] alphabetCells = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        public Board()
        {
            grid = new Cell[10, 10];
            shipsList = new List<Ship>();

            initializeBoard();
        }

        private void initializeBoard()
        {
            for (int i = 0; i < kGridLength; i++)
            {
                for (int j = 0; j < kGridLength; j++)
                {
                    grid[i, j] = new Cell();
                }
            }
        }

        private int verifyPosition(int row, char column)
        {
            if (row < 0 || row >= kGridLength)
                throw new ArgumentException("Incorrect value for a row.", nameof(row));

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
                        for (int size = 1; size <= ((int)shipClass); row++, size++)
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

        private bool executeHitOnShip(Ship ship, int row, char column)
        {
            var shipPosition = ship.GetPosition();

            int shipRow = shipPosition.Values.First();
            char shipColumn = shipPosition.Keys.First();

            ShipClass shipClass = ship.GetShipClass();
            ShipDirection shipDirection = ship.GetDirection();

            switch (shipDirection)
            {
                case ShipDirection.VERTICAL:
                    {
                        for (int size = 1, i = shipRow; size <= ((int)shipClass); i++, size++)
                        {
                            if (i == row && shipColumn == column)
                            {
                                ship.Hit();

                                if (ship.IsSunk())
                                {
                                    int columnIn = verifyPosition(row, shipColumn);
                                    markShipAreaCells(shipRow, columnIn, shipClass, shipDirection, CellStatus.HIT);
                                    return true;
                                }
                            }
                        }
                        break;
                    }
                case ShipDirection.HORIZONTAL:
                    {
                        for (int size = 1, j = shipColumn; size <= ((int)shipClass); j++, size++)
                        {
                            if (shipRow == row && j == column)
                            {
                                ship.Hit();

                                if (ship.IsSunk())
                                {
                                    int columnIn = verifyPosition(row, shipColumn);
                                    markShipAreaCells(shipRow, columnIn, shipClass, shipDirection, CellStatus.HIT);
                                    return true;
                                }
                            }
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid value for a Direction ship.");
            }

            return false;
        }

        public string Draw()
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

        public string DrawHide()
        {
            const string horizontalSeparator = "\n---+---+---+---+---+---+---+---+---+---+---+";
            StringBuilder board = new StringBuilder();

            board.Append(" X |");
            board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(i => $" {alphabetCells[i]} |")));

            for (int i = 0; i < kGridLength; i++)
            {
                board.AppendLine(horizontalSeparator);
                board.Append($" {i} |");

                board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(j => grid[i, j].ToStringHide())));
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
                Console.WriteLine($"\nError: {ex.Message}");
                return false;
            }
        }

        public bool MoveCheck(int row, char column)
        {       
            int columnIndex = verifyPosition(row, column);

            CellStatus state = grid[row, columnIndex].GetCellStatus();

            if (state == CellStatus.HIT || state == CellStatus.MISS)
                throw new ArgumentException("Cannot be re-fired");

            return state != CellStatus.HIT;
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

                            foreach (Ship ship in shipsList)
                            {
                                if(executeHitOnShip(ship, row, column))
                                {
                                    Console.Write("Sank the ship ");
                                    return true;
                                }
                            }
                            return true;
                        }
                    case CellStatus.EMPTY:
                        grid[row, columnIndex].UpdateCellStatus(CellStatus.MISS);
                        break;
                    case CellStatus.FORBIDDEN:
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

        public int GetAliveShipsCount()
        {
            int aliveShipCount = 0;
            foreach (var ship in shipsList)
            {
                if (!ship.IsSunk())
                    aliveShipCount++;
            }
            return aliveShipCount;
        }

        public void Clear()
        {
            shipsList.Clear();

            for (int i = 0; i < kGridLength; i++)
            {
                for (int j = 0; j < kGridLength; j++)
                {
                    grid[i, j].UpdateCellStatus(CellStatus.EMPTY);
                }
            }
        }
    }
}