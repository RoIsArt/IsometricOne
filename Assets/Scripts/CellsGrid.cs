using System;
using UnityEngine;
using Zenject;

public class CellsGrid : MonoBehaviour
{
    private CellsGridConfig _config;
    private Cell[,] _cells;
    private Vector2Int _sourcePosition;

    public CellsGridConfig Config { get { return _config; } }
    public Cell[,] Cells { get { return _cells; } }
    public Vector2Int SourcePosition { get { return _sourcePosition; } }

    public Cell this[Vector2Int index]
    {
        get => _cells[index.x, index.y];
    }

    [Inject]
    public void Construct(CellsGridConfig cellsGridConfig)
    {
        _config = cellsGridConfig;
        _cells = new Cell[_config.GridSize.x, _config.GridSize.y];
        _sourcePosition = _config.SourcePosition;
    }

    public void AddCellInGrid(Cell cellToAdd)
    {
        var index = cellToAdd.Index;
        var cellOnGrid = _cells[index.x, index.y];

        if (cellOnGrid.Data.Type == CellType.EMPTY)
        {
            GameObject.Destroy(cellOnGrid.gameObject);
            _cells[index.x, index.y] = cellToAdd;
            PlacedCellOnPosition(cellToAdd);
        }
    }

    public void PlacedCellOnPosition(Cell cell)
    {
        var index = cell.Index;
        if (CheckOutOfRangeIndex(index))
        {
            throw new ArgumentOutOfRangeException("Grid not contain this position");
        }

        _cells[index.x, index.y] = cell;
        cell.gameObject.transform.parent = this.gameObject.transform;
        cell.gameObject.transform.position = GetCellPosition(index);
    }

    public bool CheckOutOfRangeIndex(Vector2Int index)
    {
        return index.x < 0 || index.x >= _cells.GetLength(0)
            || index.y < 0 || index.y >= _cells.GetLength(1);
    }

    public Vector2Int GetIndexFromPixel(Vector2 pixel)
    {
        var xIndex = Mathf.RoundToInt((pixel.x /_config.CellWidth + pixel.y /_config.CellHeight)/2);
        var yIndex = Mathf.RoundToInt((pixel.y /_config.CellHeight - pixel.x /_config.CellWidth)/2);

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
        return indexInArray.x * _config.RightBasis + indexInArray.y * _config.LeftBasis;
    }
}
