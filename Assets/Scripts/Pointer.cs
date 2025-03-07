using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : IService
{
    private CellsGrid _cellsGrid;
    private Cell _pointedCell;

    private EventBus _eventBus;

    public void Init()
    {
        _cellsGrid = ServiceLocator.Instance.Get<CellsGrid>();
        _eventBus = ServiceLocator.Instance.Get<EventBus>();
        _pointedCell = null;
    }

    public void PointToCell()
    {
        Vector2 mousePosition = GetMousePosition();
        Vector2Int cellIndex = _cellsGrid.GetIndexFromPixel(mousePosition);
        bool isOutOfRange = _cellsGrid.CheckOutOfRangeIndex(cellIndex);

        if (!isOutOfRange)
        {
            var cell = _cellsGrid[cellIndex];

            if (_pointedCell != cell)
            {
                SetPointedCell(new OnCellPointedEvent(cell));
                return;
            }                 
        }
        else
        {
            SetPointedCell(new OnCellPointedEvent(null));
        }
    }

    private void SetPointedCell(OnCellPointedEvent pointedEvent)
    {
        _pointedCell = pointedEvent.Cell;
        _eventBus.Invoke(pointedEvent);
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 inGameMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return (Vector2)inGameMousePosition;
    }
}
