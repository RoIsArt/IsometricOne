namespace GroundState
{
    public abstract class GroundStateDecorator : IUpdatableGroundState
    {
        private readonly GroundStateMachine _groundStateMachine;
        private readonly IUpdatableGroundState _baseState;

        protected GroundStateDecorator(GroundStateMachine groundStateMachine, IUpdatableGroundState groundState)
        {
            _baseState = groundState;
            _groundStateMachine = groundStateMachine;
        }

        public virtual void Enter()
        {
            _baseState.Enter();
        }

        public virtual void Update()
        {
            _baseState.Update();
        }

        public virtual void Exit()
        {
            _baseState.Exit();
        }
    }
}