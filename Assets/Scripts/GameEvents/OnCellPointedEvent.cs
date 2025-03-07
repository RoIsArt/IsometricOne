using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCellPointedEvent
{
    public readonly Cell Cell;

    public OnCellPointedEvent(Cell cell)
    {
        Cell = cell;
    }
}
