using System.Collections;
using UnityEngine;

public class OnChangeGroundStateEvent
{
    public readonly IGroundState State;

    public OnChangeGroundStateEvent(IGroundState state)
    {
        State = state;
    }
}
