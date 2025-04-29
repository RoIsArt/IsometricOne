using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartBuildingCellEvent
{
    public readonly CellData BuildData;

    public OnStartBuildingCellEvent(CellData cellData)
    {
        BuildData = cellData;
    }   
}
