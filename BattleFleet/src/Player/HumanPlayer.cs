using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class HumanPlayer : Player
    {
        public HumanPlayer(string playerName) : base(playerName) { }

        public override void Initialize(Board ownBoard, Board opponentBoard)
        {
            this.ownBoard = ownBoard;
            this.opponentBoard = opponentBoard;
        }

        public override void MakeMove()
        {
            Console.WriteLine($"Хід гравця {playerName}.");

            // Логіка для введення координат від гравця через консоль
            // Обробка ходу та оновлення дошки
        }
    }

}
