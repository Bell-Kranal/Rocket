using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadUI() =>
            LoadObject(AssetsPath.UIPath);

        public GameObject LoadPlayer() =>
            LoadObject(AssetsPath.PlayerPath);

        public GameObject LoadObstacles() =>
            LoadObject(AssetsPath.ObstaclesPath);

        private GameObject LoadObject(string path)
        {
            GameObject gameObject = Resources.Load<GameObject>(path);

            return gameObject;
        }
    }
}