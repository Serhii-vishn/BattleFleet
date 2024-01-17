namespace BattleFleet.src.PlayerBoard
{
    public class Cell
    {
        private CellStatus cellStatus;
        private string cellCode;

        public Cell()
        {
            cellStatus = CellStatus.EMPTY;
            cellCode = $"   |";
        }

        public CellStatus GetCellStatus()
        {
            return cellStatus;
        }

        public override string ToString()
        {
            return cellCode;
        }

        public string ToStringHide()
        {
            if (cellStatus == CellStatus.OCCUPIED)
                return $"   |";

            return cellCode;
        }

        public bool UpdateCellStatus(CellStatus newStatusCell)
        {
            if (cellStatus == CellStatus.HIT)
                return false;

            this.cellStatus = newStatusCell;
            updateCellCode(newStatusCell);

            return true;
        }

        private void updateCellCode(CellStatus newStatusCell)
        {
            switch (newStatusCell)
            {
                case CellStatus.EMPTY:
                    cellCode = $"   |";
                    break;
                case CellStatus.OCCUPIED:
                    cellCode = $" ■ |";
                    break;
                case CellStatus.HIT:
                    cellCode = $" \u001b[31mX\u001b[0m |";
                    break;
                case CellStatus.MISS:
                    cellCode = $" \u001b[36m-\u001b[0m |";
                    break;
                case CellStatus.FORBIDDEN:
                    cellCode = $"   |";
                    break;
                case CellStatus.SANK_FORBIDEN:
                    cellCode = $" \u001b[37mX\u001b[0m |";
                    break;
                default:
                    break;
            }
        }
    }
}