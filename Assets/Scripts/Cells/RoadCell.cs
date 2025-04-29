using UnityEngine;

public class RoadCell : ConnectingCell
{
    protected int _minePerSecond;

    public void Construct(RoadCellData data, Vector2Int index)
    {
        base.Construct(data, index);
        _minePerSecond = data.MinePerSecond;
    }
}
