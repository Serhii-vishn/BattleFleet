using System.Text;

namespace BattleFleet.src.PlayerBoard
{
    public class Board
    {
        private const int k_GridLength = 10;
        private readonly Cell[,] _grid;
        private readonly List<Ship> _shipsList;
        private readonly char[] _alphabetCells = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        public Board()
        {
            _grid = new Cell[10, 10];
            _shipsList = new List<Ship>();

            InitializeBoard();
        }

        public string GetBoardToString()
        {
            const string horizontalSeparator = "\n---+---+---+---+---+---+---+---+---+---+---+";
            StringBuilder board = new();

            board.Append(" X |");
            board.Append(string.Join("", Enumerable.Range(0, k_GridLength).Select(i => $" {_alphabetCells[i]} |")));

            for (int i = 0; i < k_GridLength; i++)
            {
                board.AppendLine(horizontalSeparator);
                board.Append($" {i} |");

                board.Append(string.Join("", Enumerable.Range(0, k_GridLength).Select(j => _grid[i, j].ToString())));
            }

            return board.ToString();
        }

        public string DrawHide()
        {
            const string horizontalSeparator = "\n---+---+---+---+---+---+---+---+---+---+---+";
            StringBuilder board = new();

            board.Append(" X |");
            board.Append(string.Join("", Enumerable.Range(0, k_GridLength).Select(i => $" {_alphabetCells[i]} |")));

            for (int i = 0; i < k_GridLength; i++)
            {
                board.AppendLine(horizontalSeparator);
                board.Append($" {i} |");

                board.Append(string.Join("", Enumerable.Range(0, k_GridLength).Select(j => _grid[i, j].ToStringHide())));
            }

            return board.ToString();
        }

        public bool MovePlaceShip(int row, char column, ShipClass shipClass, ShipDirection shipDirection)
        {
            try
            {
                CanCreateNewShip(shipClass);

                int columnIndex = VerifyPosition(row, column);
                CanPlaceShip(row, columnIndex, shipClass, shipDirection);

                MarkShipAreaCells(row, columnIndex, shipClass, shipDirection, CellStatus.FORBIDDEN);

                switch (shipDirection)
                {
                    case ShipDirection.VERTICAL:
                        {
                            for (int size = 1, i = row; size <= ((int)shipClass); i++, size++)
                            {
                                _grid[i, columnIndex].UpdateCellStatus(CellStatus.OCCUPIED);
                            }
                            break;
                        }
                    case ShipDirection.HORIZONTAL:
                        {
                            for (int size = 1, i = columnIndex; size <= ((int)shipClass); i++, size++)
                            {
                                _grid[row, i].UpdateCellStatus(CellStatus.OCCUPIED);
                            }
                            break;
                        }
                    default:
                        throw new ArgumentException("Invalid value for a Direction ship.");
                }
                _shipsList.Add(new Ship(shipClass, new Dictionary<char, int> { { column, row } }, shipDirection));

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
            int columnIndex = VerifyPosition(row, column);

            CellStatus state = _grid[row, columnIndex].GetCellStatus();

            if (state == CellStatus.HIT || state == CellStatus.MISS)
                throw new ArgumentException("Cannot be re-fired");

            return state != CellStatus.HIT;
        }

        public bool MoveShoot(int row, char column)
        {
            try
            {
                int columnIndex = VerifyPosition(row, column);

                CellStatus state = _grid[row, columnIndex].GetCellStatus();
                switch (state)
                {
                    case CellStatus.OCCUPIED:
                        {
                            _grid[row, columnIndex].UpdateCellStatus(CellStatus.HIT);

                            foreach (Ship ship in _shipsList)
                            {
                                if (ExecuteHitOnShip(ship, row, column))
                                {
                                    Console.Write("Sank the ship ");
                                    return true;
                                }
                            }
                            return true;
                        }
                    case CellStatus.EMPTY:
                        _grid[row, columnIndex].UpdateCellStatus(CellStatus.MISS);
                        break;
                    case CellStatus.FORBIDDEN:
                        _grid[row, columnIndex].UpdateCellStatus(CellStatus.MISS);
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
            foreach (var ship in _shipsList)
            {
                if (!ship.IsSunk())
                    aliveShipCount++;
            }
            return aliveShipCount;
        }

        public void Clear()
        {
            _shipsList.Clear();

            for (int i = 0; i < k_GridLength; i++)
            {
                for (int j = 0; j < k_GridLength; j++)
                {
                    _grid[i, j].UpdateCellStatus(CellStatus.EMPTY);
                }
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < k_GridLength; i++)
            {
                for (int j = 0; j < k_GridLength; j++)
                {
                    _grid[i, j] = new Cell();
                }
            }
        }

        private int VerifyPosition(int row, char column)
        {
            if (row < 0 || row >= k_GridLength)
                throw new ArgumentException("Incorrect value for a row.", nameof(row));

            int columnIndex = -1;
            for (int i = 0; i < k_GridLength; i++)
            {
                if (_alphabetCells[i] == char.ToUpper(column))
                    columnIndex = i;
            }
            if (columnIndex == -1)
                throw new ArgumentException("Incorrect value for a column.", nameof(column));

            return columnIndex;
        }

        private void CanCreateNewShip(ShipClass shipClass)
        {
            int numberShips = 0;

            foreach (Ship ship in _shipsList)
            {
                if (ship.GetShipClass() == shipClass)
                {
                    numberShips++;
                }
            }

            switch (shipClass)
            {
                case ShipClass.FIVE_DECK:
                    {
                        if (numberShips >= 1)
                            throw new InvalidOperationException("Cannot place more than 1 ship of FIVE_DECK class.");

                        break;
                    }
                case ShipClass.THREE_DECK:
                    {
                        if (numberShips >= 2)
                            throw new InvalidOperationException("Cannot place more than 2 ships of THREE_DECK class.");

                        break;
                    }
                case ShipClass.TWO_DECK:
                    {
                        if (numberShips >= 3)
                            throw new InvalidOperationException("Cannot place more than 3 ships of TWO_DECK class.");

                        break;
                    }
                case ShipClass.ONE_DECK:
                    {
                        if (numberShips >= 4)
                            throw new InvalidOperationException("Cannot place more than 4 ships of ONE_DECK class.");

                        break;
                    }
                default:
                    throw new ArgumentException("Invalid ship class");
            }
        }

        private void CanPlaceShip(int row, int column, ShipClass shipClass, ShipDirection shipDirection)
        {
            switch (shipDirection)
            {
                case ShipDirection.VERTICAL:
                    {
                        for (int size = 1; size <= ((int)shipClass); row++, size++)
                        {
                            if (_grid[row, column].GetCellStatus() != CellStatus.EMPTY)
                            {
                                if (_grid[row, column].GetCellStatus() == CellStatus.OCCUPIED)
                                    throw new ArgumentException("Сan't put a ship in its way, there is another ship");
                                else if (_grid[row, column].GetCellStatus() == CellStatus.FORBIDDEN)
                                    throw new ArgumentException("Сan't place a ship, it occupies the free zone of another ship");
                            }
                        }
                        break;
                    }
                case ShipDirection.HORIZONTAL:
                    {
                        for (int size = 1; size <= ((int)shipClass); column++, size++)
                        {
                            if (_grid[row, column].GetCellStatus() == CellStatus.OCCUPIED)
                                throw new ArgumentException("Сan't put a ship in its way, there is another ship");
                            else if (_grid[row, column].GetCellStatus() == CellStatus.FORBIDDEN)
                                throw new ArgumentException("Сan't place a ship, it occupies the free zone of another ship");
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid value for a Direction ship.");
            }
        }

        private void MarkShipAreaCells(int row, int column, ShipClass shipClass, ShipDirection shipDirection, CellStatus newCellStatus)
        {
            switch (shipDirection)
            {
                case ShipDirection.HORIZONTAL:
                    {
                        int startRow = Math.Max(row - 1, 0);
                        int endRow = Math.Min(row + 1, k_GridLength - 1);
                        int startColumn = Math.Max(column - 1, 0);
                        int endColumn = Math.Min(column + (int)shipClass, k_GridLength - 1);

                        for (int i = startRow; i <= endRow; i++)
                        {
                            for (int j = startColumn; j <= endColumn; j++)
                            {
                                _grid[i, j].UpdateCellStatus(newCellStatus);
                            }
                        }
                        break;
                    }
                case ShipDirection.VERTICAL:
                    {
                        int startRow = Math.Max(row - 1, 0);
                        int endRow = Math.Min(row + (int)shipClass, k_GridLength - 1);
                        int startColumn = Math.Max(column - 1, 0);
                        int endColumn = Math.Min(column + 1, k_GridLength - 1);

                        for (int i = startRow; i <= endRow; i++)
                        {
                            for (int j = startColumn; j <= endColumn; j++)
                            {
                                _grid[i, j].UpdateCellStatus(newCellStatus);
                            }
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid value for a Direction ship.");
            }
        }

        private bool ExecuteHitOnShip(Ship ship, int row, char column)
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
                                    int columnIn = VerifyPosition(row, shipColumn);
                                    MarkShipAreaCells(shipRow, columnIn, shipClass, shipDirection, CellStatus.SANK_FORBIDEN);
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
                                    int columnIn = VerifyPosition(row, shipColumn);
                                    MarkShipAreaCells(shipRow, columnIn, shipClass, shipDirection, CellStatus.SANK_FORBIDEN);
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
    }
}