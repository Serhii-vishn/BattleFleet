using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleFleet.src.Board
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

                board.Append(string.Join("", Enumerable.Range(0, kGridLength).Select(j => grid[i, j].GetCellCode())));
            }

            return board.ToString();
        }



        //private void CreateShips()
        //{
        //    shipsList.Add(new Ship())
        //}
        //public bool placeShip(Ship)
        //{

        //}

    }
}
