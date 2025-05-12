using Cells;

namespace GameEvents
{
    public class OnStartBuildingCellEvent
    {
        public readonly CellType CellType;

        public OnStartBuildingCellEvent(CellType cellType)
        {
            CellType = cellType;
        }   
    }
}
