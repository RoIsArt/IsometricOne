using GamePlayServices;
using UnityEngine.Windows;
using UnityEngine;
using Input = UnityEngine.Input;

namespace GroundState
{
    public class BuildingState : IUpdatableGridState
    {
        private readonly GridStateMachine _gridStateMachine;
        private readonly IUpdatableGridState _baseState;
        private readonly IHighlighter _highlighter;
        private readonly IBuilder _builder;

        public BuildingState(GridStateMachine gridStateMachine, IUpdatableGridState baseState, IHighlighter highlighter, IBuilder builder)
        {
            _gridStateMachine = gridStateMachine;
            _baseState = baseState;
            _highlighter = highlighter;
            _builder = builder;
        }

        public void Enter()
        {
            _highlighter.StartBuild();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Update()
        {
            _baseState.Update();
            _builder.Plan();

            if (Input.GetMouseButtonDown(0))
            {
                _builder.Build();
                _gridStateMachine.Enter<MiningState>();
            }
        }
        
        public void Exit()
        {
            _highlighter.EndBuild();
        }

    }
}