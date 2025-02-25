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
        Vector2 mousePosition = GetMousePosition();
        Vector2Int cellIndex = _cellsGrid.GetIndexFromPixel(mousePosition);  
        bool isOutOfRange = _cellsGrid.CheckOutOfRangeIndex(cellIndex);

        if (!isOutOfRange)
        {
            Cell pointedCell = _cellsGrid[cellIndex];

            if (_highlightedCell != null)
            {
                if (pointedCell.Index != _highlightedCell.Index)
                {
                    ChangeSelectedFrameActivity();
                    _highlightedCell = pointedCell;
                    ChangeSelectedFrameActivity();
                }
            }
            else
            {
                _highlightedCell = pointedCell;
                ChangeSelectedFrameActivity();
            }
        }
        else if(_highlightedCell != null)
        {
            ClearHighlighting();
        }        
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 inGameMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return (Vector2)inGameMousePosition;
    }

    private void ChangeSelectedFrameActivity()
    {
        bool isActive = !_highlightedCell.Selected.activeSelf;
        _highlightedCell.Selected.SetActive(isActive);
    }

    private void ClearHighlighting()
    {
        _highlightedCell.Selected.SetActive(false);
        _highlightedCell = null;
    }
}
