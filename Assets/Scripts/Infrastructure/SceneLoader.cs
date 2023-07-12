using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly LoadingSlider _loadingSlider;
        private readonly LoadingScreen _loadingScreen;

        public SceneLoader(LoadingSlider loadingSlider, LoadingScreen loadingScreen)
        {
            _loadingSlider = loadingSlider;
            _loadingScreen = loadingScreen;
        }

        public void Load(string name, Action<string> onLoaded = null) =>
            LoadScene(name, onLoaded);

        private async Task LoadScene(string sceneName, Action<string> action = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(sceneName))
            {
                action?.Invoke(sceneName);
                return;
            }
      
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!loadOperation.isDone)
            {
                _loadingSlider.SetFillAmount(loadOperation.progress);
                await Task.Yield();
            }
            
            _loadingSlider.SetFillAmount(1f);
            _loadingScreen.Hide();
            loadOperation.allowSceneActivation = true;
            action?.Invoke(sceneName);
        }
    }
}