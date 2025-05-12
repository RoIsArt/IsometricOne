using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using GamePlayServices;
using Infrastructure.Factories;
using Infrastructure.Game;
using UnityEngine;

namespace GroundState
{
    public class GroundStateMachine : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly Dictionary<Type, IGroundState> _states;
        private IGroundState _currentState;
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _currentRoutine;

        public GroundStateMachine(IEventBus eventBus, 
            IGridFactory gridFactory,
            IHighlighter highlighter,
            IBuilder builder, 
            ICoroutineRunner coroutineRunner)
        {
            _eventBus = eventBus;
            _coroutineRunner = coroutineRunner;
            
            IUpdatableGroundState miningState = new MiningState(this);
            _states = new Dictionary<Type, IGroundState>()
            {
                [typeof(InitialState)] = new InitialState(this, gridFactory, eventBus),
                [typeof(MiningState)] = miningState,
                [typeof(BuildingState)] = new BuildingState(this, miningState, builder, highlighter, eventBus)
            };
            
            _eventBus.Subscribe<OnStartBuildingCellEvent>(EnterBuildState);
            _eventBus.Subscribe<OnCellBuildedEvent>(EnterMiningState);
        }
        

        public void Enter<TState>() where TState : class, IGroundState
        {
            TState state = ChangeState<TState>();
            state.Enter();

            if (_currentState is IUpdatableGroundState updatableState)
            {
                _currentRoutine = _coroutineRunner.StartRoutine(UpdateState(updatableState));
            }
        }
        
        private TState ChangeState<TState>() where TState : class, IGroundState
        {
            if (_currentRoutine != null)
                _coroutineRunner.StopRoutine(_currentRoutine);
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }
        
        private IEnumerator UpdateState(IUpdatableGroundState state)
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                state.Update();
            }
        }

        private void EnterBuildState(OnStartBuildingCellEvent onChangeGroundStateEvent)
        {
            Enter<BuildingState>();
        }
        
        private void EnterMiningState(OnCellBuildedEvent onCellBuildedEvent)
        {
            Enter<MiningState>();
        }

        private TState GetState<TState>() where TState : class, IGroundState => 
            _states[typeof(TState)] as TState;


        public void Dispose()
        {
            _eventBus.Unsubscribe<OnStartBuildingCellEvent>(EnterBuildState);
            _eventBus.Unsubscribe<OnCellBuildedEvent>(EnterMiningState);
        }
    }
}

