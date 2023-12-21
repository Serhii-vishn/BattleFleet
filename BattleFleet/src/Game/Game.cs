using System;

namespace BattleFleet.src.Game
{
    using BattleFleet.src.PlayerBoard;
    using BattleFleet.src.Player;

    class Game
    {
        private Board player1Board;
        private Board player2Board;
        private Player currentPlayer;

        public Game(Player player1, Player player2)
        {
            player1Board = new Board();
            player2Board = new Board();

           // player1.Initialize(player1Board, player2Board);
           // player2.Initialize(player2Board, player1Board);

            currentPlayer = player1;
        }

        public void StartGame()
        {

        }

        public void SwitchTurn()
        {
            //currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        public void EndGame()
        {

        }

        public bool IsGameOver()
        {

            return false;
        }
    }
}
