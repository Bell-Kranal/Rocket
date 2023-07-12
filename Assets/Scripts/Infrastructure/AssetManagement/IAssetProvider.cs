using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        public GameObject LoadUI();
        public GameObject LoadPlayer();
        public GameObject LoadObstacles();
    }
}