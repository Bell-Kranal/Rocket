namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        public void CreateUI();
        public void CreatePlayer(string sceneName);
        public void CreateObstacles(string sceneName);
        public void Reload();
    }
}