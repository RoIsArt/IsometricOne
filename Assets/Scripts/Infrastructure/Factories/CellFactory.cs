using Cells;
using GameEvents;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class CellFactory : ICellFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IEventBus _eventBus;
        private CellsGrid _grid;
        
        [Inject]
        public CellFactory(IStaticDataService staticDataService, IEventBus eventBus)
        {
            _staticDataService = staticDataService;
            _eventBus = eventBus;
        }
        
        public Cell Create(CellType type, Vector2Int index)
        {
            var data = _staticDataService.ForCell(type);
            var cellInstance = GameObject.Instantiate(data.Prefab, _grid.transform);
            var cell = cellInstance.GetComponent<Cell>();
            cell.Construct(data, index, _eventBus);
            _grid.AddCellInGrid(cell);
            return cell;
        }

        public void AssignGrid(CellsGrid grid)
        {
            _grid = grid;
        }
    }
}
