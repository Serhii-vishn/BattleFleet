using System;

namespace BattleFleet.src.PlayerBoard
{
    class Cell
    {
        private int row;
        private char column;
        private CellStatus cellStatus;
        private string cellCode;

        public Cell(int row, char column)
        {
            this.row = row;
            this.column = column;
            cellStatus = CellStatus.EMPTY;
            cellCode = $"   |";
        }
        public Cell(int row, char column, CellStatus cellStatus)
        {
            this.row = row;
            this.column = column;
            this.cellStatus = cellStatus;
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
                    cellCode = $" X |";
                    break;
                case CellStatus.MISS:
                    cellCode = $" - |";
                    break;
                case CellStatus.FORBIDDEN:
                    cellCode = $"   |";
                    break;
                default:
                    break;
            }
        }

        public int Row
        {
            get { return row; }
        }

        public char Column
        {
            get { return column; }
        }

        public CellStatus GetCellStatus()
        {
            return cellStatus;
        }

        public string ToString()
        {
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
    }
}