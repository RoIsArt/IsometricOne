using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GroundStateMachine : MonoBehaviour, IDisposable
{
    private CellsGrid _cellsGrid;
    private GridGenerator _gridGenerator;
    private CellFactory _cellFactory;
    private IState _currentState;
    private EventBus _eventBus;

    private void Update()
    {
        _currentState?.Update();
    }

    [Inject]
    public void Construct(CellsGrid cellsGrid, 
                        CellFactory cellFactory,  
                        EventBus eventBus,
                        MiningState startState)
    {
        _cellsGrid = cellsGrid;
        _cellFactory = cellFactory;
        _eventBus = eventBus;

        _gridGenerator = new GridGenerator(_cellsGrid, _cellFactory);
        _gridGenerator.Generate();

        _eventBus.Subscribe<OnChangeGroundStateEvent>(SetState);
        _eventBus.Invoke<OnChangeGroundStateEvent>(new OnChangeGroundStateEvent(startState));
    }

    public void SetState(OnChangeGroundStateEvent changeGroundStateEvent)
    {
        _currentState?.Exit();
        _currentState = changeGroundStateEvent.State;
        _currentState.Enter();
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnChangeGroundStateEvent>(SetState);
    }
}

