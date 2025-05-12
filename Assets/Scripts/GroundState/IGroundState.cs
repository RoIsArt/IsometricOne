namespace GroundState
{
    public interface IGroundState
    {
        void Enter();
        void Exit();
    }

    public interface IUpdatableGroundState : IGroundState
    {
        void Update();
    }
}
