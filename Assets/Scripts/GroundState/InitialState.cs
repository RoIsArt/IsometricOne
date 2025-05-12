using Cells;
using GameEvents;
using Infrastructure.Factories;

namespace GroundState
{
    public class InitialState : IGroundState
    {
        private readonly GroundStateMachine _groundStateMachine;
        private readonly IGridFactory _gridFactory;
        private readonly IEventBus _eventBus;

        public InitialState(GroundStateMachine groundStateMachine, IGridFactory gridFactory, IEventBus eventBus)
        {
            _groundStateMachine = groundStateMachine;
            _gridFactory = gridFactory;
            _eventBus = eventBus;
        }

        public void Enter()
        {
            _gridFactory.Create(out CellsGrid grid);
            _eventBus.Invoke<OnGridInitializedEvent>(new OnGridInitializedEvent(grid));
            _groundStateMachine.Enter<MiningState>();
        }

        public void Exit()
        {

        }
    }
}
