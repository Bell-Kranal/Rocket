using System;
using System.Collections.Generic;
using Infrastructure.Factories;
using Infrastructure.States;
using Logic;
using Zenject;

namespace Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;

        public GameStateMachine(DiContainer diContainer, SceneLoader sceneLoader, LevelModel levelModel)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(diContainer, sceneLoader, this, levelModel),
                [typeof(LoadLevelState)] = new LoadLevelState(diContainer.Resolve<IGameFactory>()),
                [typeof(ReloadLevelState)] = new ReloadLevelState(diContainer.Resolve<IGameFactory>(), levelModel),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}