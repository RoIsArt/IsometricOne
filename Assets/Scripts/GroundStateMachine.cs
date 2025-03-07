using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundStateMachine : MonoBehaviour, IService
{
    private CellsGrid _cellsGrid;
    private GridGenerator _gridGenerator;
    private CellFactory _cellFactory;
    private List<IState> _groundStates;
    private IState _currentState;

    private void Update()
    {
        _currentState?.Update();
    }

    public void Init()
    {
        _cellsGrid = ServiceLocator.Instance.Get<CellsGrid>();
        _cellFactory = ServiceLocator.Instance.Get<CellFactory>();
        _gridGenerator = new GridGenerator(_cellsGrid, _cellFactory);
        _gridGenerator.Generate();

        var pointer = ServiceLocator.Instance.Get<Pointer>();
        var highlighter = ServiceLocator.Instance.Get<Highlighter>();
        var builder = ServiceLocator.Instance.Get<Builder>();


        _groundStates = new List<IState>()
        {
            new MiningState(pointer),
            new BuildingState(builder, highlighter)
        };

        var startState = _groundStates.FirstOrDefault(x => x.GetType() == typeof(MiningState));
        SetState(startState);
    }

    public void SetState(IState nextState)
    {
        bool isStateContain = _groundStates.Contains(nextState);

        if (isStateContain == false)
        {
            throw new Exception("State is not contains");
        }

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }
}

