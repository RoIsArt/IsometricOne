using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningState : IState
{
    private Pointer _pointer;

    public MiningState(Pointer pointer)
    {
        _pointer = pointer;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        _pointer.PointToCell();
    }
}
