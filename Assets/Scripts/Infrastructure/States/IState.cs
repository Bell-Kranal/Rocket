namespace Infrastructure.States
{
    public interface IState : IExitableState
    {
        public void Enter();
        
    }
}