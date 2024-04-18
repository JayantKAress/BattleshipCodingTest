namespace BattleShipStateTracker.Common.Service
{
    public interface IBattleShipService
    {
        public void CreateBoard();
        public string ShowBoard();
        public string BoardArrayToString();
        public string MockBoardArrayToString();
        public void AddShip(int totalShips);
        public void FireShip(int x = 0, int y = 0);
        public string HitOrMissShip();
        public bool WinGame();
        public void RestartGame();
    }
}
