using System;
using UnityEngine;

public class CellsGrid : MonoBehaviour
{
    private CellsDataConfig _cellsDataConfig;
    private GridConfig _cellGridConfig;
    private CellSizeConfig _cellSizeConfig;
    private Cell[,] _cells;
    private Vector2Int _sourcePosition;
    private RouteConstructor _routeConstructor;

    public void Construct(EventBus eventBus)
    { 
        _cells = new Cell[_cellGridConfig.GridSize.x, _cellGridConfig.GridSize.y];
        _sourcePosition = _cellGridConfig.SourcePosition;
        _routeConstructor = new RouteConstructor(this, CreateRoute(), eventBus);
    }

    public Cell[,] Cells { get { return _cells; } }
    public Vector2Int SourcePosition { get { return _sourcePosition; } }
    public Cell this[Vector2Int index]
    {
        get
        {
            if (!CheckOutOfRangeIndex(index))
            {
                return _cells[index.x, index.y];
            }
            else
            {
                return null;
            }
        }
    }

    public void AddCellInGrid(GameObject cellToAdd)
    {
        var cellComponent = cellToAdd.GetComponent<Cell>();
        var index = cellComponent.Index;
        var cellOnGrid = _cells[index.x, index.y];

        if (cellOnGrid.Data.Type == CellType.EMPTY)
        {
            GameObject.Destroy(cellOnGrid.gameObject);
            _cells[index.x, index.y] = cellComponent;
            PlacedCellOnPosition(cellToAdd);
        }
    }

    public void PlacedCellOnPosition(GameObject cell)
    {
        var cellComponent = cell.GetComponent<Cell>();
        var index = cellComponent.Index;

        if (CheckOutOfRangeIndex(index))
        {
            throw new ArgumentOutOfRangeException("Grid not contain this position");
        }

        _cells[index.x, index.y] = cellComponent;
        cell.transform.parent = this.gameObject.transform;
        cell.transform.position = GetCellPosition(index);
    }

    public bool CheckOutOfRangeIndex(Vector2Int index)
    {
        return index.x < 0 || index.x >= _cells.GetLength(0)
            || index.y < 0 || index.y >= _cells.GetLength(1);
    }

    public Vector2Int GetIndexFromPixel(Vector2 pixel)
    {
        var xIndex = Mathf.RoundToInt((pixel.x / _cellSizeConfig.CellWidth + pixel.y / _cellSizeConfig.CellHeight)/2);
        var yIndex = Mathf.RoundToInt((pixel.y / _cellSizeConfig.CellHeight - pixel.x / _cellSizeConfig.CellWidth)/2);

        var isOutOfRange = CheckOutOfRangeIndex(new Vector2Int(xIndex, yIndex));

        if (isOutOfRange)
        {
            return new Vector2Int(-1, -1);
        }
        else
        {
            return new Vector2Int(xIndex, yIndex);
        }
    }

    private Vector2 GetCellPosition(Vector2Int indexInArray)
    {
        return indexInArray.x * _cellSizeConfig.RightBasis + indexInArray.y * _cellSizeConfig.LeftBasis;
    }

    private Route CreateRoute()
    {
        return new Route((ConnectingCell)this[_sourcePosition]);
    }
}
