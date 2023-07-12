using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelStaticDataPath = "LevelStaticData/";
        
        private Dictionary<string,LevelStaticData> _levelsData;
        
        public void Load()
        {
            _levelsData = Resources
                .LoadAll<LevelStaticData>(LevelStaticDataPath)
                .ToDictionary(x => x.SceneName, x => x);
        }

        public LevelStaticData ForLevel(string sceneName) =>
            _levelsData.TryGetValue(sceneName, out LevelStaticData staticData) ? staticData : null;
    }
}