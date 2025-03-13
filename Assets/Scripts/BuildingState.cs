using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuildingState : IState
{
    private readonly Builder _builder;
    private readonly Highlighter _highlighter;
    private readonly Pointer _pointer;
    private readonly MiningState _miningState;
    private readonly EventBus _eventBus;

    public BuildingState(Builder builder, 
                        Highlighter highlighter,   
                        Pointer pointer)
    {
        _builder = builder;
        _highlighter = highlighter;
        _pointer = pointer;
    }

    public void Enter()
    {
        Action<Cell> action = _highlighter.HighlightForBuild;
        _highlighter.SetHighlightMethod(action);
        _highlighter.HighlightEmptyCells();
    }

    public void Update()
    {
        _pointer.PointToCell();

        if(Input.GetMouseButtonDown(0))
        {
            _builder.BuildCell();
        }
    }

    public void Exit()
    {
        _highlighter.ClearAllHighlighting();
    }
}
