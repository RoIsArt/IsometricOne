namespace Infrastructure.GameStates
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
