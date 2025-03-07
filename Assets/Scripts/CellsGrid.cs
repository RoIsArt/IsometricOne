using System;
using UnityEngine;

public class CellsGrid : MonoBehaviour, IService
{
    [SerializeField] private CellsGridConfig _config;
    private Cell[,] _cells;

    public CellsGridConfig Config { get { return _config; } }
    public Cell[,] Cells { get { return _cells; } }

    public Cell this[Vector2Int index]
    {
        get => _cells[index.x, index.y];
    }

    public void Init()
    {
        _cells = new Cell[_config.GridSize.x, _config.GridSize.y];
    }

    public void PlacedCellOnPosition(Cell cell, Vector2Int index)
    {
        bool isOutOfRange = CheckOutOfRangeIndex(index);

        if (isOutOfRange)
        {
            throw new ArgumentOutOfRangeException("Grid not contain this position");
        }

        _cells[index.x, index.y] = cell;
        cell.transform.parent = this.transform;
        cell.transform.position = GetCellPosition(index);
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
