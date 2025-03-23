using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Builder : IDisposable
{
    private CellsGrid _cellsGrid;
    private CellFactory _cellFactory;
    private MiningState _miningState;
    private EventBus _eventBus;

    private CellData _cellDataForBuild;
    private Cell _pointedCell;

    public Builder(CellsGrid cellsGrid, CellFactory cellFactory, EventBus eventBus, MiningState miningState)
    {
        _cellsGrid = cellsGrid;
        _cellFactory = cellFactory;
        _eventBus = eventBus;
        _miningState = miningState;

        _eventBus.Subscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        _eventBus.Subscribe<OnCellPointedEvent>(SetPointedForBuildingCell);
        _eventBus.Subscribe<OnCellBuildedEvent>(CompleteBulding);
    }

    public void BuildCell()
    {
        if(_pointedCell == null) return;
        
        var index = _pointedCell.Index;
        var cell = _cellFactory.CreateCell(_cellDataForBuild.Type, index);
        _cellsGrid.AddCellInGrid(cell);
        _eventBus.Invoke(new OnCellBuildedEvent(_cellDataForBuild));   
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnStartBuildingCellEvent>(PrepareForBuilding);
        _eventBus.Unsubscribe<OnCellPointedEvent>(SetPointedForBuildingCell);
        _eventBus.Unsubscribe<OnCellBuildedEvent>(CompleteBulding);
    }

    private void PrepareForBuilding(OnStartBuildingCellEvent onStartBuildingCellEvent)
    {
        _cellDataForBuild = onStartBuildingCellEvent.CellData;
    }

    private void SetPointedForBuildingCell(OnCellPointedEvent onCellPointedEvent)
    {
        _pointedCell = onCellPointedEvent.Cell;
    }

    private void CompleteBulding(OnCellBuildedEvent onCellBuildedEvent)
    {
        _cellDataForBuild = null;
        _eventBus.Invoke(new OnCellPointedEvent(null));
        _eventBus.Invoke(new OnChangeGroundStateEvent(_miningState));
    }
}
