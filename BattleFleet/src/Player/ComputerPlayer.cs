﻿using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    class ComputerPlayer : Player
    {
        public ComputerPlayer() : base() { }

        public override void ClearBoard()
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

            // Логіка для автоматичного визначення координат ходу комп'ютера
            // Обробка ходу та оновлення дошки
            return false;
        }

        public override void PlaceShips()
        {
            throw new NotImplementedException();
        }

        public override void PlaceShipsTemplate(List<string> shipsTemplate)
        {
            throw new NotImplementedException();
        }
    }
}