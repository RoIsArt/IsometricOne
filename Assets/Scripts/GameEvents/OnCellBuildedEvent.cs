using UnityEngine;

public class OnCellBuildedEvent
{
    public readonly CellData CellData;

    public OnCellBuildedEvent(CellData cellData) 
    {
        CellData = cellData; 
    }
}
