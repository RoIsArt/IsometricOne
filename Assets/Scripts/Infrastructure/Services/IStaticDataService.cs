using Cells;
using DatasAndConfigs;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadCells();
        void LoadGrid();
        
        CellData ForCell(CellType type);
        GridConfig ForGrid();
    }
}