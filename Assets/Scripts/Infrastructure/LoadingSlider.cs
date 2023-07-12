using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    [RequireComponent(typeof(Image))]
    public class LoadingSlider : MonoBehaviour
    {
        private Image _image;

        public void GetImage() =>
            _image = GetComponent<Image>();

        public void SetFillAmount(float value) =>
            _image.fillAmount = value;
    }
}