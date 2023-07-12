namespace Infrastructure.States
{
    public interface IPayloadState<TPayLoad> : IExitableState
    {
        public void Enter(TPayLoad payload);
    }
}