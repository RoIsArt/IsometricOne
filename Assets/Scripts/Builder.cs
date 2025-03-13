using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : IDisposable
{
    private CellsGrid _cellsGrid;
    private CellFactory _cellFactory;
    private MiningState _miningState;
    private EventBus _eventBus;

    private CellData _cellForBuild;
    private Cell _pointedForBuildingCell;

    public Builder(CellsGrid cellsGrid, CellFactory cellFactory, EventBus eventBus, MiningState miningState)
    {
        _cellsGrid = cellsGrid;
        _cellFactory = cellFactory;
        _eventBus = eventBus;
        _miningState = miningState;

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
        if(_cellForBuild != null && _pointedForBuildingCell != null)
        {
            var index = _pointedForBuildingCell.Index;
            var cell = _cellFactory.CreateCell(_cellForBuild.Type, index);
            _cellsGrid.AddCellInGrid(cell, index);
            _cellForBuild = null;
            _eventBus.Invoke(new OnCellPointedEvent(null));
            _eventBus.Invoke(new OnChangeGroundStateEvent(_miningState));
        }
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        _eventBus.Unsubscribe<OnCellPointedEvent>(SetPointedForBuildingCell);
    }
}
