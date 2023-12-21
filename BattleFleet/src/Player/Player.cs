using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    abstract class Player
    {
        protected string playerName;
        protected Board ownBoard;
        protected Board opponentBoard;

        public Player(string playerName)
        {
            this.playerName = playerName;
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public abstract void Initialize(Board ownBoard, Board opponentBoard);

        public abstract void MakeMove();
    }
}