using Cells;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ICellFactory
    {
        void SetGrid(CellsGrid grid);
        GameObject Create(CellType type, Vector2Int index);
    }
}