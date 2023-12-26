using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class ComputerPlayer : Player
    {
        public ComputerPlayer() : base() { }

        public override void DrawBoard()
        {
            throw new NotImplementedException();
        }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override void MakeMove()
        {

            // Логіка для автоматичного визначення координат ходу комп'ютера
            // Обробка ходу та оновлення дошки
        }

        public override void PlaceShips()
        {
            throw new NotImplementedException();
        }
    }
}
