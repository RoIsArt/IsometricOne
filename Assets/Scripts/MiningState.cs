using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiningState : IState
{
    private readonly Pointer _pointer;
    private readonly Highlighter _highlighter;
    private readonly Miner _miner;

    public MiningState(Pointer pointer, Highlighter highlighter)
    {
        _pointer = pointer;
        _highlighter = highlighter;
    }

    public void Enter()
    {
        Action<Cell> action = _highlighter.HighlightForMine;
        _highlighter.SetHighlightMethod(action);
        //Coroutines.Instance.StartCoroutine(_miner.Mine());

    }

    public void Update()
    {
        _pointer.PointToCell();
    }

    public void Exit()
    {

    }
}
