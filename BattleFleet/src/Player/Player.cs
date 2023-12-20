using BattleFleet.src.PlayerBoard;
using System;

namespace BattleFleet.src.Player
{
    abstract class Player
    {
        protected string playerName;

        public Player(string playerName)
        { 
            this.playerName = playerName; 
        }

        public string GetPlayerName()
        { 
            return playerName; 
        }

        public abstract void MakeMove(Board opponentBoard);
    }
}