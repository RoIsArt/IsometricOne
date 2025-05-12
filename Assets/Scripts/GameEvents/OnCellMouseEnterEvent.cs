using Cells;

namespace GameEvents
{
    public class OnCellMouseEnterEvent
    {
        public readonly Cell Cell;

        public OnCellMouseEnterEvent(Cell cell)
        {
            Cell = cell;
        }
    }
}
