using Cells;
using DatasAndConfigs;
using GameEvents;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class CellFactory : ICellFactory
    {
        private readonly IEventBus _eventBus;
        private readonly IStaticDataService _staticDataService;
        private CellsGrid _grid;
        
        [Inject]
        public CellFactory(IStaticDataService staticDataService, IEventBus eventBus)
        {
            _staticDataService = staticDataService;
            _eventBus = eventBus;
        }

        public void SetGrid(CellsGrid grid)
        {
            _grid = grid;
        }
        
        public GameObject Create(CellType type, Vector2Int index)
        {
            CellData data = _staticDataService.ForCell(type);
            GameObject cellObj = GameObject.Instantiate(data.prefab);
            Cell cell = cellObj.GetComponent<Cell>();
            
            cell.Construct(data, index, _eventBus);
            _grid.AddCell(cell);
            
            return cellObj;
        }
    }
}
