namespace GroundState
{
    public class InitialState : IGridState
    {
        private readonly GridStateMachine _gridStateMachine;

        public InitialState(GridStateMachine gridStateMachine)
        {
            _gridStateMachine = gridStateMachine;
        }

        public void Enter()
        {
            _gridStateMachine.Enter<MiningState>();
        }

        public void Exit()
        {
        }
    }
}