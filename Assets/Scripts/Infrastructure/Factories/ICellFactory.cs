using Cells;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ICellFactory
    {
        GameObject Create(CellType type, Vector2Int index);
    }
}