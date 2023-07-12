using Infrastructure.States;

namespace Infrastructure
{
    public interface IGameStateMachine
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayloadState<TPayLoad>;
    }
}