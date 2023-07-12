using System;
using Infrastructure;
using Infrastructure.States;
using Logic.Obstacles;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerCollision : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Obstacle obstacle))
            {
                GetComponent<PlayerMover>().enabled = false;
                _gameStateMachine.Enter<ReloadLevelState>();
            }
        }
    }
}