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
            GridConfig config = _staticDataService.ForGrid();
            GameObject grid = CreateGrid(config);
            Vector2Int sourcePosition = GetSourcePosition(config);

            InitializeGrid(config, sourcePosition);

            Vector3 gridPos = grid.transform.position;
            gridPos.y += config.Shift.y;
            grid.transform.position = gridPos;
            
            return grid;
        }
        
        private GameObject CreateGrid(GridConfig config)
        {
            GameObject gridObj = _assetProvider.Instantiate(AssetPath.GridPath);
            CellsGrid cellsGrid = gridObj.GetComponent<CellsGrid>();
            cellsGrid.Construct(config);
            _cellFactory.SetGrid(cellsGrid);
            
            
            
            _eventBus.Invoke(new OnCellsGridCreatedEvent(cellsGrid));
            return gridObj;
        }

        private static Vector2Int GetSourcePosition(GridConfig config)
        {
            return new Vector2Int(
                Random.Range(1, config.GridSize.x - 1),
                Random.Range(1, config.GridSize.y - 1));
        }
        
        private void InitializeGrid(GridConfig config, Vector2Int sourcePosition)
        {
            for (int x = 0; x < config.GridSize.x; x++)
            {
                for (int y = 0; y < config.GridSize.y; y++)
                {
                    if (x == sourcePosition.x && y == sourcePosition.y)
                        _cellFactory.Create(CellType.Source, new Vector2Int(x, y));
                    else
                        _cellFactory.Create(CellType.Empty, new Vector2Int(x, y));
                }
            }
        }
    }
}

