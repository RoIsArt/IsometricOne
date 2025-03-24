using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SourceCellData", menuName = "Cells/SourceCellData")]
public class SourceCellData : CellData
{
    [SerializeField] private List<ConnectSide> _connectSides;

    public List<ConnectSide> ConnectSides { get { return _connectSides; } }
}
