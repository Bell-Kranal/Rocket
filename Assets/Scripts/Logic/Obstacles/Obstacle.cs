using Logic.Player;
using UnityEngine;

namespace Logic.Obstacles
{
    public class Obstacle : MonoBehaviour, IHaveFirstPosition
    {
        public Vector3 FirstPosition { get; set; }
        
        public void GoToFirstPosition() =>
            transform.position = FirstPosition;

        private void Awake() =>
            FirstPosition = transform.position;
    }
}