using UnityEngine;

namespace Cells
{
    public enum SideName
    {
        None,
        North,
        South,
        East,
        West
    }
    
    public static class SideNameExtensions
    {
        public static SideName GetOpposite(this SideName side)
        {
            return side switch
            {
                SideName.North => SideName.South,
                SideName.South => SideName.North,
                SideName.East => SideName.West,
                SideName.West => SideName.East,
                _ => SideName.None
            };
        }
        
        public static Vector2Int ToDirection(this SideName side)
        {
            return side switch
            {
                SideName.North => new Vector2Int(1, 0),
                SideName.South => new Vector2Int(-1, 0),
                SideName.East => new Vector2Int(0, -1),
                SideName.West => new Vector2Int(0, 1),
                _ => Vector2Int.zero
            };
        }
    }
}
