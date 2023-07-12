using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level Data")]
    public class LevelStaticData : ScriptableObject
    {
        public string SceneName;
        public float Speed;
        public Vector3 PlayerSpawnPosition;
        public Vector3 ObstacleSpawnPosition;
    }
}