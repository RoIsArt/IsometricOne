using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Highlighter
{
    private CellsGrid _cellsGrid;
    private Cell _highlightedCell;

    public Highlighter(CellsGrid cellsGrid)
    {
        _cellsGrid = cellsGrid;
    }

    public void HighlightCell()
    {
        var mousePosition = GetMousePosition();
        var cellIndex = _cellsGrid.GetIndexFromPixel(mousePosition);  
        bool outOfRange = _cellsGrid.CheckOutOfRangeIndex(cellIndex);

        if (!outOfRange)
        {
            var cell = _cellsGrid[cellIndex];

            if (_highlightedCell != null)
            {
                if (cell.Index != _highlightedCell.Index)
                {
                    ChangeSelectedFrameActivity();
                    _highlightedCell = cell;
                    ChangeSelectedFrameActivity();
                }
            }
            else
            {
                _highlightedCell = cell;
                ChangeSelectedFrameActivity();
            }
        }
        else
        {
            ClearHighLight();
        }        
    }

    private Vector2 GetMousePosition()
    {
        var mouseScreenPosition = Input.mousePosition;
        var inGameMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return new Vector2(inGameMousePosition.x, inGameMousePosition.y);
    }

    private void ChangeSelectedFrameActivity()
    {
        var selectedFrame = _highlightedCell?.Selected;

        if (selectedFrame != null)
        {
            var active = selectedFrame.activeSelf ? false : true;
            selectedFrame.SetActive(active);
        }
    }

    private void ClearHighLight()
    {
        ChangeSelectedFrameActivity();
        _highlightedCell = null;
    }
}
