using UnityEngine;

namespace Logic.Player
{
    public interface IHaveFirstPosition
    {
        public Vector3 FirstPosition { get; set; }
        
        public void GoToFirstPosition();
    }
}