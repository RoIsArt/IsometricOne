using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Callback
{
    public readonly object Action;

    public Callback(object action)
    {
        Action = action;
    }
}
