using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    private readonly CellsGrid _cellsGrid;
    private readonly CellFactory _cellFactory;
    private Vector2Int _sourcePosition;

    public GridGenerator(CellsGrid cellsGrid, CellFactory cellFactory)
    {
        _cellFactory = cellFactory;
        _cellsGrid = cellsGrid;
        _sourcePosition = _cellsGrid.SourcePosition;
    }

    public void Generate()
    {
        for (int x = 0; x < _cellsGrid.Config.GridSize.x; x++)
            for (int y = 0; y < _cellsGrid.Config.GridSize.y; y++)
            {
                CellType type;

                if(x == _sourcePosition.x && y == _sourcePosition.y)
                {
                    type = CellType.SOURCE;  
                }
                else
                {
                    type = CellType.EMPTY;
                }

                Cell cell = _cellFactory.CreateCell(type, new Vector2Int(x, y));
                PlacedOnPosition(cell);
            }
    }

    private void PlacedOnPosition(Cell cell)
    {
        _cellsGrid.PlacedCellOnPosition(cell);
    }
}

