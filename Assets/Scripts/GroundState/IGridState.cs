namespace GroundState
{
    public interface IGridState
    {
        void Enter();
        void Exit();
    }

    public interface IUpdatableGridState : IGridState
    {
        void Update();
    }
}
