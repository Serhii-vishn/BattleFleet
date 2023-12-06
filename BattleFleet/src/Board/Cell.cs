using System;

namespace BattleFleet.src.Board
{
    class Cell
    {
        private int row;
        private char column;
        private CellStatus cellStatus;

        Cell(int row, char column)
        {
            this.row = row;
            this.column = column;
            cellStatus = CellStatus.EMPTY;
        }

        Cell(int row, char column, CellStatus cellStatus)
        {
            this.row = row;
            this.column = column;
            this.cellStatus = cellStatus;
        }

        public CellStatus GetCellStatus()
        {
            return cellStatus;
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