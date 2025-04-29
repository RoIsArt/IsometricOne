using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundStateMachine : MonoBehaviour
{
    private EventBus _eventBus;
    private Dictionary<Type, IGroundState> _states;
    private IGroundState _currentState;

    private void Awake()
    {

    }
    private void Update()
    {
        _currentState?.Update();
    }

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
        _states = new Dictionary<Type, IGroundState>()
        {

        };
        _eventBus.Invoke(new OnChangeGroundStateEvent(_currentState));
    }

    public void SetState<TState>() where TState : IGroundState
    {
        _currentState?.Exit();
        _currentState = _states[typeof(TState)];
        _currentState.Enter();
    }
}

