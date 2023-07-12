using UnityEngine;

namespace Logic.Obstacles
{
    public class ObstaclePositionChecker : MonoBehaviour
    {
        private Camera _mainCamera;
        private float _screenTopEdge;
        private float _screenBottomEdge;

        private void Awake()
        {
            _mainCamera = Camera.main;

            _screenTopEdge = _mainCamera.ViewportToWorldPoint(new Vector3 (1f, 1f, 10f)).y;
            _screenBottomEdge = _mainCamera.ScreenToWorldPoint(new Vector3 (0, 0, 10f)).y;
        }

        private void Update()
        {
            if (transform.position.y < _screenBottomEdge)
            {
                MoveBlockUp();
            }
        }

        private void MoveBlockUp() =>
            transform.position = new Vector3(transform.position.x, _screenTopEdge, transform.position.z);
    }
}