using Cells;

namespace GameEvents
{
    public class OnCellBuildedEvent
    {
        public readonly CellType CellType;

        public OnCellBuildedEvent(CellType cellType)
        {
            CellType = cellType;
        }
    }
}
