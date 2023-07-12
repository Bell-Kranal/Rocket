using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Logic;
using Zenject;

namespace Infrastructure.States
{
    public class BootstrapState : IPayloadState<string>
    {
        private readonly DiContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly LevelModel _levelModel;

        public BootstrapState(DiContainer diContainer, SceneLoader sceneLoader, IGameStateMachine gameStateMachine, LevelModel levelModel)
        {
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _levelModel = levelModel;

            RegisterServices();
        }

        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit() { }

        private void RegisterServices()
        {
            IStaticDataService staticDataService = new StaticDataService();
            IGameFactory gameFactory = new GameFactory(new AssetProvider(), staticDataService, _diContainer, _levelModel);
            staticDataService.Load();

            _diContainer.Bind<IGameFactory>().FromInstance(gameFactory).AsSingle();
            _diContainer.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
        }

        private void OnLoaded(string sceneName) =>
            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
    }
}