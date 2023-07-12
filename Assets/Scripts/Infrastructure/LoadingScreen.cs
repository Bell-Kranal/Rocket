using DG.Tweening;
using UnityEngine;

namespace Infrastructure
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            gameObject.SetActive(true);
        }

        public void Hide() =>
            _canvasGroup
                .DOFade(0f, _fadeDuration)
                .OnComplete(
                    () => gameObject.SetActive(false)
                );
    }
}