using System.Collections.Generic;
using System.Linq;

namespace Cells
{
    public enum CellType
    {
        Empty,
        Source,
        RoadNe,
        RoadNs,
        RoadSe,
        RoadWe,
        RoadWn,
        RoadWs
    }
    
    public static class CellTypeExtensions
    {
        private static readonly HashSet<CellType> Connectables = new HashSet<CellType>()
        {
            CellType.Source,
            CellType.RoadNe,
            CellType.RoadNs,
            CellType.RoadSe,
            CellType.RoadWe,
            CellType.RoadWn,
            CellType.RoadWs
        };
        
        public static IEnumerable<SideName> GetSides(this CellType type)
        {
            return type switch
            {
                CellType.Source => new[] { SideName.North, SideName.South, SideName.East, SideName.West },
                CellType.RoadNe => new[] { SideName.North, SideName.East },
                CellType.RoadNs => new[] { SideName.North, SideName.South },
                CellType.RoadSe => new[] { SideName.South, SideName.East },
                CellType.RoadWe => new[] { SideName.West, SideName.East },
                CellType.RoadWn => new[] { SideName.West, SideName.North },
                CellType.RoadWs => new[] { SideName.West, SideName.South },
                _ => Enumerable.Empty<SideName>()
            };
        }
        
        public static bool IsConnectable(this CellType type) => 
            Connectables.Contains(type);
    }
}
