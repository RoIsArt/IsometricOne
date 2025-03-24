using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoadCellData", menuName = "Cells/RoadCellData")]
public class RoadCellData : CellData
{
    [SerializeField] private List<ConnectSide> _connectSides;
    [SerializeField] private int miningPerSecond;

    public int MiningPerSecond { get { return miningPerSecond; } }
    public List<ConnectSide> ConnectSides { get {  return _connectSides; } }
}


