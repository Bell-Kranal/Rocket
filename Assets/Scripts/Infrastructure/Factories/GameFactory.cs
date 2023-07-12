using System.Collections.Generic;
using Data;
using Infrastructure.AssetManagement;
using Logic;
using Logic.Obstacles;
using Logic.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _diContainer;
        private readonly LevelModel _levelModel;
        private readonly List<IHaveFirstPosition> _iHaveFirstPositionList;

        private TapToPlayHandler _tapToPlayHandler;
        private PlayerMover _playerMover;
        private LoosePanel _loosePanel;
        private ObstacleMover[] _obstacleMovers;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, DiContainer diContainer, LevelModel levelModel)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _diContainer = diContainer;
            _levelModel = levelModel;
            _iHaveFirstPositionList = new List<IHaveFirstPosition>();
        }

        public void CreateUI()
        {
            GameObject ui = _assetProvider.LoadUI();
            GameObject instantiatedUI = _diContainer.InstantiatePrefab(ui);

            _tapToPlayHandler = instantiatedUI.GetComponentInChildren<TapToPlayHandler>();
            _levelModel.LoosePanel = instantiatedUI.GetComponentInChildren<LoosePanel>();
            _levelModel.RestartButton = instantiatedUI.GetComponentInChildren<Button>();

            _levelModel.LoosePanel.gameObject.SetActive(false);
        }

        public void CreatePlayer(string sceneName)
        {
            GameObject player = _assetProvider.LoadPlayer();
            LevelStaticData levelStaticData = _staticDataService.ForLevel(sceneName);

            _playerMover = _diContainer
                            .InstantiatePrefab(player, levelStaticData.PlayerSpawnPosition, Quaternion.Euler(player.transform.eulerAngles), null)
                            .GetComponentInChildren<PlayerMover>();
            
            _playerMover.enabled = false;
            _tapToPlayHandler.MouseDown += () =>
            {
                _playerMover.enabled = true;
            };

            _iHaveFirstPositionList.Add(_playerMover.GetComponent<IHaveFirstPosition>());
        }

        public void CreateObstacles(string sceneName)
        {
            GameObject obstacles = _assetProvider.LoadObstacles();
            LevelStaticData levelStaticData = _staticDataService.ForLevel(sceneName);

            _obstacleMovers = _diContainer
                .InstantiatePrefab(obstacles, levelStaticData.ObstacleSpawnPosition, Quaternion.Euler(obstacles.transform.eulerAngles), null)
                .GetComponentsInChildren<ObstacleMover>();

            foreach (ObstacleMover obstacle in _obstacleMovers)
            {
                obstacle.Init(levelStaticData.Speed);
                obstacle.enabled = false;
                _tapToPlayHandler.MouseDown += () =>
                {
                    obstacle.enabled = true;
                };

                _iHaveFirstPositionList.Add(obstacle.GetComponent<IHaveFirstPosition>());
            }
        }

        public void Reload()
        {
            _playerMover.Reset();
            
            foreach (IHaveFirstPosition element in _iHaveFirstPositionList)
            {
                element.GoToFirstPosition();
            }

            foreach (ObstacleMover obstacle in _obstacleMovers)
            {
                obstacle.enabled = false;
            }
            
            _tapToPlayHandler.gameObject.SetActive(true);
        }
    }
}