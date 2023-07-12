using UnityEngine;

namespace Logic.Obstacles
{
    public class ObstacleMover : MonoBehaviour
    {
        private float _speed;

        public void Init(float speed) =>
            _speed = speed;

        private void Update()
        {
            transform.position += Vector3.down * _speed * Time.deltaTime;
        }
    }
}