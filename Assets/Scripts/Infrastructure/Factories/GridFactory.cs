using AssetManagment;
using Cells;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public  class GridFactory : IGridFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICellFactory _cellFactory;
        private readonly IAssetProvider _assetProvider;

        [Inject]
        public GridFactory(ICellFactory cellFactory, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _cellFactory = cellFactory;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public GameObject Create(out CellsGrid cellsGrid)
        {
            GameObject grid = _assetProvider.Instantiate(AssetPath.GridPath);
            cellsGrid = grid.GetComponent<CellsGrid>();
            cellsGrid.Construct(_staticDataService.ForGrid());
            _cellFactory.AssignGrid(cellsGrid);
            var gridConfig = _staticDataService.ForGrid();

            for (int x = 0; x < gridConfig.GridSize.x; x++)
                for (int y = 0; y < gridConfig.GridSize.y; y++)
                {
                    if (x == gridConfig.SourcePosition.x && y == gridConfig.SourcePosition.y)
                        _cellFactory.Create(CellType.Source, new Vector2Int(x, y));
                    else
                        _cellFactory.Create(CellType.Empty, new Vector2Int(x, y));
                }
            
            return grid;
        }
    }
}

