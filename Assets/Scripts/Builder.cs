using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : IDisposable
{
    private CellsGrid _cellsGrid;
    private CellFactory _cellFactory;
    private EventBus _eventBus;

    private CellData _cellForBuild;
    private Cell _pointedForBuildingCell;

    public Builder(CellsGrid cellsGrid, CellFactory cellFactory, EventBus eventBus)
    {
        _cellsGrid = cellsGrid;
        _cellFactory = cellFactory;
        _eventBus = eventBus;

        _eventBus.Subscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        _eventBus.Subscribe<OnCellPointedEvent>(SetPointedForBuildingCell);
    }

    public void PrepareForBuilding(OnStartBuildingCellEvent onStartBuildingCellEvent)
    {
        _cellForBuild = onStartBuildingCellEvent.CellData;
    }

    public void SetPointedForBuildingCell(OnCellPointedEvent onCellPointedEvent)
    {
        _pointedForBuildingCell = onCellPointedEvent.Cell;
    }

    public void BuildCell()
    {
        if( _cellForBuild != null && _pointedForBuildingCell != null)
        {
            var index = _pointedForBuildingCell.Index;
            var cell = _cellFactory.CreateCell(_cellForBuild.Type, index);
            _cellsGrid.AddCellInGrid(cell, index);
        }
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        _eventBus.Unsubscribe<OnCellPointedEvent>(SetPointedForBuildingCell);
    }
}
