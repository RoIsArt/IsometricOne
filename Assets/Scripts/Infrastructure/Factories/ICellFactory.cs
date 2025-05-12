using Cells;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ICellFactory
    {
        Cell Create(CellType type, Vector2Int index);
        void AssignGrid(CellsGrid grid);
    }
}