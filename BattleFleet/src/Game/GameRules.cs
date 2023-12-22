namespace BattleFleet.src.Game
{
    interface GameRules
    {
        public void StartGame();
        public void EndGame();
        public void SwitchTurn();
        public bool IsGameOver();
    }
}