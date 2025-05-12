using GameEvents;
using GamePlayServices;
using Infrastructure.Services;
using UnityEngine;

namespace GroundState
{
    public class BuildingState : GroundStateDecorator
    {
        private readonly GroundStateMachine _groundStateMachine;
        private readonly IBuilder _builder;
        private readonly IHighlighter _highlighter;
        private readonly IEventBus _eventBus;

        [Inject]
        public BuildingState(GroundStateMachine groundStateMachine, 
            IUpdatableGroundState baseState, 
            IBuilder builder, 
            IHighlighter highlighter, IEventBus eventBus) : base(groundStateMachine, baseState)
        {
            _builder = builder;
            _highlighter = highlighter;
            _eventBus = eventBus;
        }

        public override void Enter()
        {
            base.Enter();
            _highlighter.StartBuild();
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0))
            {
                var cell = _builder.Build();
                _eventBus.Invoke(new OnCellBuildedEvent(cell));
            }
        }

        public override void Exit()
        {
            base.Exit();
            _highlighter.EndBuild();
        }
    }
}
