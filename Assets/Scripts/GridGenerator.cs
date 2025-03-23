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
                CellType type;

                if(x == _cellsGrid.Config.GridSize.x - 1 &&
                    y == _cellsGrid.Config.GridSize.y - 1)
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

