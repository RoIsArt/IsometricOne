using System;
using System.Collections.Generic;
using Cells;
using GamePlayServices;


namespace GroundState
{
    public class GridStateMachine
    {
        private readonly CellsGrid _grid;
        private readonly Dictionary<Type, IGridState> _states;

        private IGridState _currentState;
        
        public GridStateMachine(CellsGrid grid, 
                                IHighlighter highlighter, 
                                IMiner miner, 
                                IBuilder builder)
        {
            _grid = grid;

            IUpdatableGridState miningState = new MiningState(_grid.Cells, miner);
            
            _states = new Dictionary<Type, IGridState>()
            {
                [typeof(InitialState)] = new InitialState(this),
                [typeof(MiningState)] = miningState,
                [typeof(BuildingState)] = new BuildingState(this, miningState,highlighter, builder)
            };
        }

        public void Enter<TState>() where TState : class, IGridState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IGridState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            if (state is IUpdatableGridState updatableState)
            {
                _grid.SetState(updatableState);
            }
            else
            {
                _grid.SetState(null);
            }
            return state;
        }

        private TState GetState<TState>() where TState : class, IGridState => 
            _states[typeof(TState)] as TState;
    }
}