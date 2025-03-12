using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiningState : IState
{
    private Pointer _pointer;
    private Highlighter _highlighter;

    public MiningState(Pointer pointer, Highlighter highlighter)
    {
        _pointer = pointer;
        _highlighter = highlighter;
    }

    public void Enter()
    {
        Action<Cell> action = _highlighter.HighlightForMine;
        _highlighter.SetHighlightMethod(action);
    }

    public void Update()
    {
        _pointer.PointToCell();
    }

    public void Exit()
    {

    }
}
