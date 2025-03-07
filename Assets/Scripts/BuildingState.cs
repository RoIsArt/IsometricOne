using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : IState
{
    private Builder _builder;
    private Highlighter _highlighter;

    public BuildingState(Builder builder, Highlighter highlighter)
    {
        _builder = builder;
        _highlighter = highlighter;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}
