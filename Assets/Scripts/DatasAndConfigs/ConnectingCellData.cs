using System.Collections.Generic;
using UnityEngine;

public class ConnectingCellData : CellData
{
    [SerializeField] private List<ConnectingSide> _connectSides;
    public List<ConnectingSide> ConnectingSides { get { return _connectSides; } }
}
