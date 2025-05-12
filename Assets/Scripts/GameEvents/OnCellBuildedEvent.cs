using Cells;

namespace GameEvents
{
    public class OnCellBuildedEvent
    {
        public readonly Cell Cell;

        public OnCellBuildedEvent(Cell cell) 
        {
            Cell = cell; 
        }
    }
}
