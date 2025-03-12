using System.Collections;
using UnityEngine;

public class OnChangeGroundStateEvent
{
    public readonly IState State;

    public OnChangeGroundStateEvent(IState state)
    {
        State = state;
    }
}
