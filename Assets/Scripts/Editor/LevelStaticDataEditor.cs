using Data;
using Logic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string PlayerSpawnPositionTag = "PlayerSpawnPosition";
        private const string ObstacleSpawnPositionTag = "ObstacleSpawnPosition";
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.SceneName = SceneManager.GetActiveScene().name;
                levelData.PlayerSpawnPosition = GameObject.FindGameObjectWithTag(PlayerSpawnPositionTag).transform.position;
                levelData.ObstacleSpawnPosition = GameObject.FindGameObjectWithTag(ObstacleSpawnPositionTag).transform.position;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}