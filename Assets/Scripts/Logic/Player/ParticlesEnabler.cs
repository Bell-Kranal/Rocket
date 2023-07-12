using UnityEngine;

namespace Logic.Player
{
    public class ParticlesEnabler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _fire;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _fire.Play();
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _fire.Stop();
                }
            }
        }
    }
}