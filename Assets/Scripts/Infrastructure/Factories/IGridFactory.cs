using Cells;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGridFactory
    {
        GameObject Create(out CellsGrid grid);
    }
}