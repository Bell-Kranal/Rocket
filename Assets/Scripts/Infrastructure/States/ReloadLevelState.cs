using Infrastructure.Factories;
using Logic;

namespace Infrastructure.States
{
    public class ReloadLevelState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly LevelModel _levelModel;

        public ReloadLevelState(IGameFactory gameFactory, LevelModel levelModel)
        {
            _gameFactory = gameFactory;
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _levelModel.LoosePanel.gameObject.SetActive(true);
            _levelModel.RestartButton.onClick.AddListener(() =>
            {
                _gameFactory.Reload();
                _levelModel.LoosePanel.gameObject.SetActive(false);
                _levelModel.RestartButton.onClick.RemoveAllListeners();
            });
        }

        public void Exit() { }
    }
}