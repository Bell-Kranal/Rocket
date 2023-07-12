using System;
using UnityEngine;

namespace Logic
{
    public class TapToPlayHandler : MonoBehaviour
    {
        public event Action MouseDown;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDown?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}