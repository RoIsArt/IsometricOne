using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuildingState : IState
{
    private Builder _builder;
    private Highlighter _highlighter;
    private Pointer _pointer;

    public BuildingState(Builder builder, Highlighter highlighter, Pointer pointer)
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
        
    }
}
