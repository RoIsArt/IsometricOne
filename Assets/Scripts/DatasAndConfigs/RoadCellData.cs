using UnityEngine;

public class RoadCellData : ConnectingCellData
{
    [SerializeField] private int _minePerSecond;
    public int MinePerSecond { get { return _minePerSecond; } }
}
