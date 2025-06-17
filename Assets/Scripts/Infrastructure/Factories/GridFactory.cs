using System;
using AssetManagment;
using Cells;
using DatasAndConfigs;
using GameEvents;
using Infrastructure.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Factories
{
    public  class GridFactory : IGridFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICellFactory _cellFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IEventBus _eventBus;

        [Inject]
        public GridFactory(ICellFactory cellFactory, 
            IStaticDataService staticDataService, 
            IAssetProvider assetProvider, 
            IEventBus eventBus)
        {
            _cellFactory = cellFactory;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _eventBus = eventBus;
        }

        public GameObject Create()
        {
            GameObject grid = _assetProvider.Instantiate(AssetPath.GridPath);
            CellsGrid cellsGrid = grid.GetComponent<CellsGrid>();
            cellsGrid.Construct(_staticDataService.ForGrid());
            _eventBus.Invoke(new OnCellsGridCreatedEvent(cellsGrid));
            
            GridConfig gridConfig = _staticDataService.ForGrid();
            Vector2Int gridSize = gridConfig.GridSize;
            Vector2Int cellSize = gridConfig.CellSize;
            Vector2Int sourcePosition = new Vector2Int(
                Random.Range(1, gridConfig.GridSize.x - 1),
                Random.Range(1, gridConfig.GridSize.y - 1));

            for (int x = 0; x < gridSize.x; x++)
                for (int y = 0; y < gridSize.y; y++)
                {
                    CellType type;
                    if (x == sourcePosition.x && y == sourcePosition.y)
                        type = CellType.Source;
                    else
                        type = CellType.Empty;
                        
                    var cell = _cellFactory.Create(type, new Vector2Int(x, y));
                    cellsGrid.AddCell(cell.GetComponent<Cell>());
                }

            Vector3 gridPos = grid.transform.position;
            gridPos.y += gridConfig.Shift.y;
            grid.transform.position = gridPos;
            
            return grid;
        }
    }
}

