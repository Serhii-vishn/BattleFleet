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

            cellStatus = newStatusCell;
            return true;   
        }
    }
}