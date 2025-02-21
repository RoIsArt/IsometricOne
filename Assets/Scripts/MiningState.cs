using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningState : IState
{
    private Highlighter _highlighter;

    public MiningState(CellsGrid cellsGrid)
    {
        _highlighter = new Highlighter(cellsGrid);
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        _highlighter.HighlightCell();
    }
}
