using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class ComputerPlayer : Player
    {
        public ComputerPlayer() : base() { }

        public void ClearBoard()
        {
            throw new NotImplementedException();
        }

        public override void DrawBoard()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override bool MakeMove()
        {
            return false;
        }

        public override void PlaceShips()
        {
            throw new NotImplementedException();
        }
    }
}