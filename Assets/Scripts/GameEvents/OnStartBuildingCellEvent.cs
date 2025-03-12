using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartBuildingCellEvent
{
    public readonly CellData CellData;

    public OnStartBuildingCellEvent(CellData cellData)
    {
        CellData = cellData;
    }   
}
