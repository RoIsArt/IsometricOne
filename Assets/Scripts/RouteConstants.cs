using System.Collections.Generic;
using Cells;
using UnityEngine;

public static class RouteConstants
{
    public static readonly List<SideName> Sides = new()
    {
        SideName.North,
        SideName.East,
        SideName.South,
        SideName.West
    };
    
    public static readonly Dictionary<SideName, SideName> OppositeSides = new ()
    {
        { SideName.North, SideName.South },
        { SideName.East, SideName.West },
        { SideName.West, SideName.East },
        { SideName.South, SideName.North }
    };

    public static readonly Dictionary<SideName, Vector2Int> Offsets = new ()
    {
        {SideName.North, new Vector2Int(1, 0)},
        {SideName.East, new Vector2Int(0, -1)},
        {SideName.West, new Vector2Int(0, 1)},
        {SideName.South, new Vector2Int(-1, 0)}
    };

    public static readonly List<CellType> ConnectableTypes = new()
    {
        CellType.Source,
        CellType.RoadNe,
        CellType.RoadNs,
        CellType.RoadSe,
        CellType.RoadWe,
        CellType.RoadWn,
        CellType.RoadWs
    };
}