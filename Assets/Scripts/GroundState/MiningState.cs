using System;

namespace GroundState
{
    public class MiningState : IUpdatableGroundState, IDisposable
    {
        private readonly GroundStateMachine _groundStateMachine;
        
        public MiningState(GroundStateMachine groundStateMachine)
        {
            _groundStateMachine = groundStateMachine;
        }
        
        public void Enter()
        {

        }

        public void Update()
        {

        }


        public void Exit()
        {

        }

        public void Dispose()
        {

        }
    }
}
