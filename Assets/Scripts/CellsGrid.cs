using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsGrid : MonoBehaviour
{
    [SerializeField] private CellsGridConfig _config;
    private Cell[,] _cells;

    public CellsGridConfig Config { get { return _config; } }
    public Cell[,] Cells { get { return _cells; } }

    public void Init()
    {
        _cells = new Cell[_config.GridSize.x, _config.GridSize.y];
    }

    public void PlacedCellOnPosition(Cell cell, Vector2Int position)
    {
        bool outOfRange = position.x < 0 || position.x >= _cells.GetLength(0)
            || position.y < 0 || position.y >= _cells.GetLength(1);

        if (outOfRange)
        {
            throw new ArgumentOutOfRangeException("Grid not contain this position");
        }

        _cells[position.x, position.y] = cell;
        cell.transform.parent = this.transform;
        cell.transform.position = GetCellPosition(position);
    }

    private Vector2 GetCellPosition(Vector2Int indexInArray)
    {
        return indexInArray.x * Config.RightBasis + indexInArray.y * Config.LeftBasis;
    }
}
