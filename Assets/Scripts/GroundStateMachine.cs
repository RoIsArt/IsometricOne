using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundStateMachine : MonoBehaviour
{
    [SerializeField] private CellsGrid _cellsGrid;

    private GridGenerator _gridGenerator;
    private CellFactory _cellFactory;

    private List<IState> _groundStates;

    private IState _currentState;

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }

    public void Init()
    {
        _cellsGrid.Init();
        _cellFactory = new CellFactory(_cellsGrid.Config.CellDatas);
        _gridGenerator = new GridGenerator(_cellsGrid, _cellFactory);
        _gridGenerator.Generate();

        _groundStates = new List<IState>()
        {
            new MiningState(),
            new BuildingState()
        };

        var startState = _groundStates.FirstOrDefault(x => x.GetType() == typeof(MiningState));
        SetState(startState);
    }

    public void SetState(IState nextState)
    {
        var stateContain = _groundStates.Contains(nextState);
        if (stateContain == false)
        {
            throw new Exception("State is not contains");
        }

        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;
        _currentState.Enter();
    }
}

