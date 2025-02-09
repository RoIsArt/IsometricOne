using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    private CellsGrid _cellsGrid;
    private CellFactory _cellFactory;

    public GridGenerator(CellsGrid cellsGrid, CellFactory cellFactory)
    {
        _cellFactory = cellFactory;
        _cellsGrid = cellsGrid;
    }

    public void Generate()
    {
        for (int x = 0; x < _cellsGrid.Config.GridSize.x; x++)
            for (int y = 0; y < _cellsGrid.Config.GridSize.y; y++)
            {
                var cell = _cellFactory.CreateCell(CellType.EMPTY);
                PlacedOnPosition(cell, new Vector2Int(x, y));
            }
    }

    private void PlacedOnPosition(Cell cell, Vector2Int positionInArray)
    {
        _cellsGrid.PlacedCellOnPosition(cell, positionInArray);
    }
}

