using Assets.Scripts.AssetManagment;
using UnityEngine;

public class GridFactory 
{
    private readonly GridConfig _cellsGridConfig;
    private readonly CellsDataConfig _cellsDataConfig;
    private readonly CellFactory _cellFactory;

    public GridFactory(CellFactory cellFactory, GridConfig config, CellsDataConfig cellsDataConfig)
    {
        _cellFactory = cellFactory;
        _cellsGridConfig = config;
        _cellsDataConfig = cellsDataConfig;
    }

    public GameObject Create()
    {
        GameObject grid = InstantiateGrid(AssetPath.GRID_PATH);
        CellsGrid cellsGrid = grid.GetComponent<CellsGrid>();

        for (int x = 0; x < _cellsGridConfig.GridSize.x; x++)
            for (int y = 0; y < _cellsGridConfig.GridSize.y; y++)
            {
                //GameObject cell;
                //if(x == _cellsGridConfig.SourcePosition.x && y == _cellsGridConfig.SourcePosition.y)
                //{
                //    //cell = _cellFactory.CreateCell("source", new Vector2Int(x, y));
                //}
                //else
                //{
                //    //cell = _cellFactory.CreateCell("empty", new Vector2Int(x, y));
                //}

                //_cellsGrid.PlacedCellOnPosition(cell);
            }

        return grid;
    }

    private static GameObject InstantiateGrid(string gRID_PATH)
    {
        var prefab = Resources.Load<GameObject>(AssetPath.GRID_PATH);
        return Object.Instantiate(prefab);
    }
}

