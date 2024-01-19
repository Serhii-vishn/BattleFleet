namespace BattleFleet.src.PlayerBoard
{
    public class Cell
    {
        private CellStatus _cellStatus;
        private string _cellCode;

        public Cell()
        {
            _cellStatus = CellStatus.EMPTY;
            _cellCode = $"   |";
        }

        public CellStatus GetCellStatus()
        {
            return _cellStatus;
        }

        public override string ToString()
        {
            return _cellCode;
        }

        public string ToStringHide()
        {
            if (_cellStatus == CellStatus.OCCUPIED)
                return $"   |";

            return _cellCode;
        }

        public bool UpdateCellStatus(CellStatus newStatusCell)
        {
            if (_cellStatus == CellStatus.HIT)
                return false;

            this._cellStatus = newStatusCell;
            UpdateCellCode(newStatusCell);

            return true;
        }

        private void UpdateCellCode(CellStatus newStatusCell)
        {
            switch (newStatusCell)
            {
                case CellStatus.EMPTY:
                    _cellCode = $"   |";
                    break;
                case CellStatus.OCCUPIED:
                    _cellCode = $" ■ |";
                    break;
                case CellStatus.HIT:
                    _cellCode = $" \u001b[31mX\u001b[0m |";
                    break;
                case CellStatus.MISS:
                    _cellCode = $" \u001b[36m-\u001b[0m |";
                    break;
                case CellStatus.FORBIDDEN:
                    _cellCode = $"   |";
                    break;
                case CellStatus.SANK_FORBIDEN:
                    _cellCode = $" \u001b[37mX\u001b[0m |";
                    break;
                default:
                    break;
            }
        }
    }
}