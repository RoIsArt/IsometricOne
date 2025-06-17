using Cells;

namespace GameEvents
{
    public class OnCellMouseExitEvent 
    {
        public readonly Cell Cell;
        
        public OnCellMouseExitEvent(Cell cell)
        {
            Cell = cell;
        }
    }
}