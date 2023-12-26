namespace BattleFleet.src.Game
{
    interface IGameRules
    {
        public void StartGame();
        public void EndGame();
        public void SwitchTurn();
    }
}