using Cells;

namespace GroundState
{
    public class OnGridInitializedEvent
    {
        public CellsGrid CellsGrid;
        public OnGridInitializedEvent(CellsGrid grid)
        {
            CellsGrid = grid;
        }
    }
}