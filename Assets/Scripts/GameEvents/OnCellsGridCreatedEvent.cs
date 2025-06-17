using Cells;

namespace GameEvents
{
    public class OnCellsGridCreatedEvent
    {
        public readonly CellsGrid CellsGrid;
        public OnCellsGridCreatedEvent(CellsGrid cellsGrid)
        {
            CellsGrid = cellsGrid;
        }
    }
}