using Cells;
using UnityEngine;

public static class Vector2IntExtensions
{
    public static SideName ToSide(this Vector2Int direction)
    {
        (int x, int y) coordinates = (direction.x, direction.y);
        return coordinates switch
        {
            (1, 0) => SideName.North,
            (-1, 0) => SideName.South,
            (0, -1) => SideName.East,
            (0, 1) => SideName.West,
            _ => SideName.None
        };
    }
}