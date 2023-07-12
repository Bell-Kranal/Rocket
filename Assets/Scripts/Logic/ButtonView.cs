using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private float _scaleDuration;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;

            transform.DOScale(Vector3.one, _scaleDuration);
        }
    }
}