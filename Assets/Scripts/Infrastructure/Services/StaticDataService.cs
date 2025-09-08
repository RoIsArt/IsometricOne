using System.Collections.Generic;
using System.Linq;
using Cells;
using DatasAndConfigs;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<CellType, CellData> _cells;
        private GridConfig _gridConfig;

        public StaticDataService()
        {
            LoadCells();
            LoadGrid();
        }

        public void LoadCells() => 
            _cells = Resources.LoadAll<CellData>("CellDatas").ToDictionary(x => x.type);

        public void LoadGrid() => 
            _gridConfig = Resources.Load<GridConfig>("GridConfig/GridConfig");

        public CellData ForCell(CellType type) => 
            _cells.GetValueOrDefault(type);

        public GridConfig ForGrid() => _gridConfig;
    }
}