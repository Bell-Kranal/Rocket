using Infrastructure.States;
using Logic;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        private const string Game = "Game";
        
        private IGameStateMachine _stateMachine;

        public override void InstallBindings()
        {
            LoadingScreen loadingScreen = FindObjectOfType<LoadingScreen>();
            LoadingSlider loadingSlider = FindObjectOfType<LoadingSlider>();
            SceneLoader sceneLoader = new SceneLoader(loadingSlider, loadingScreen);
            
            loadingSlider.GetImage();
            
            _stateMachine = new GameStateMachine(Container, sceneLoader, new LevelModel());
            _stateMachine.Enter<BootstrapState, string>(Game);
            
            Container.Bind<IGameStateMachine>().FromInstance(_stateMachine).AsSingle();
        }
    }
}
