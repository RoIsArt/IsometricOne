using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Highlighter : IDisposable
{
    private readonly CellsGrid _cellsGrid;
    private readonly EventBus _eventBus;

    private Cell _highlightedCell;
    private CellData _cellDataForBuild;

    private Action<Cell> _highlight;

    public Highlighter(EventBus eventBus, CellsGrid cellsGrid)
    {
        _eventBus = eventBus;
        _cellsGrid = cellsGrid;
        _highlightedCell = null;
        _eventBus.Subscribe<OnCellPointedEvent>(Highlight);
        _eventBus.Subscribe<OnStartBuildingCellEvent>(SetCellForBuild);
    }

    public void HighlightEmptyCells()
    {
        foreach (var cell in _cellsGrid.Cells)
        {
            if (cell.Data.Type == CellType.EMPTY)
            {
                cell.Selected.SetActive(true);
            }
        }
    }

    public void SetHighlightMethod(Action<Cell> action)
    {
        _highlight = action;
    }

    public void HighlightForMine(Cell cell)
    {
        _highlightedCell?.Selected.SetActive(false);        
        cell?.Selected.SetActive(true);
        _highlightedCell = cell;
    }

    public void HighlightForBuild(Cell cell)
    {
        _highlightedCell?.SetBaseSprite();

        if (cell.Data.Type == CellType.EMPTY)
        {
            cell.SetSprite(_cellDataForBuild.BaseSprite);
        }

        _highlightedCell = cell;
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellPointedEvent>(Highlight);
        _eventBus.Unsubscribe<OnStartBuildingCellEvent>(SetCellForBuild);
    }

    private void Highlight(OnCellPointedEvent pointedCell)
    {
        _highlight(pointedCell.Cell);
    }

    private void SetCellForBuild(OnStartBuildingCellEvent onStartBuildingCellEvent)
    {
        _cellDataForBuild = onStartBuildingCellEvent.BuildData;
    }

    public void ClearAllHighlighting()
    {
        foreach (var cell in _cellsGrid.Cells)
        {
            if (cell.Selected.activeSelf)
            {
                cell.Selected.SetActive(false);
            }
        }
    }
}
