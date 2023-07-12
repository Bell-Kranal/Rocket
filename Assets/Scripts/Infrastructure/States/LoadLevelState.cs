using Infrastructure.Factories;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void Enter(string sceneName)
        {
            _gameFactory.CreateUI();
            _gameFactory.CreatePlayer(sceneName);
            _gameFactory.CreateObstacles(sceneName);
        }

        public void Exit() { }
    }
}