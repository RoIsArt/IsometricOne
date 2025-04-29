//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class Pointer
//{
//    private readonly CellsGrid _cellsGrid;
//    private readonly EventBus _eventBus;

//    private Cell _pointedCell;

//    public Pointer(CellsGrid cellsGrid, EventBus eventBus)
//    {
//        _cellsGrid = cellsGrid;
//        _eventBus = eventBus;
//    }

//    public void PointToCell()
//    {
//        Vector2 mousePosition = GetMousePosition();
//        Vector2Int cellIndex = _cellsGrid.GetIndexFromPixel(mousePosition);
//        bool isOutOfRange = _cellsGrid.CheckOutOfRangeIndex(cellIndex);

//        if (isOutOfRange)
//        {
//            SetPointedCell(new OnCellPointedEvent(null));
//            return;
//        }

//        var cell = _cellsGrid[cellIndex];

//        if (_pointedCell != cell)
//        {
//            SetPointedCell(new OnCellPointedEvent(cell));
//        }
//    }

//    private void SetPointedCell(OnCellPointedEvent pointedEvent)
//    {
//        _pointedCell = pointedEvent.Cell;
//        _eventBus.Invoke(pointedEvent);
//    }

//    private Vector2 GetMousePosition()
//    {
//        Vector3 mouseScreenPosition = Input.mousePosition;
//        Vector3 inGameMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
//        return (Vector2)inGameMousePosition;
//    }
//}
