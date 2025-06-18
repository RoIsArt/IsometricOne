using System;
using System.Collections.Generic;
using Cells;
using GameEvents;
using UnityEngine;

public class RouteConstructor : IDisposable
{
    private readonly CellsGrid _cellsGrid;
    private readonly Cell _sourceCell;
    private readonly IEventBus _eventBus;
    
    public RouteConstructor(CellsGrid cellsGrid, IEventBus eventBus, Cell sourceCell)
    {
        _eventBus = eventBus;
        _sourceCell = sourceCell;
        _cellsGrid = cellsGrid;

        _eventBus.Subscribe<OnCellBuildedEvent>(TryCreateRoute);
    }

    public void TryCreateRoute(OnCellBuildedEvent onCellBuildedEvent)
    {
        if(!RouteConstants.ConnectableTypes.Contains(onCellBuildedEvent.CellType))
            return;

        ConnectingSide unconnectedSide = GetUnconnectedSide(_sourceCell);
        Cell cellForAttached = GetNextCellForAttach(unconnectedSide);
        bool isCanAttached = cellForAttached && 
                             RouteConstants.ConnectableTypes.Contains(cellForAttached.Type);
        if(isCanAttached)
            TryAttachedCell(_sourceCell, cellForAttached, unconnectedSide);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellBuildedEvent>(TryCreateRoute);
    }

    private ConnectingSide GetUnconnectedSide(Cell cell)
    {
        return cell.GetUnconnetSide();
    }

    private Cell GetNextCellForAttach(ConnectingSide connectingSide)
    {
        Vector2Int nextCellIndex = _sourceCell.Index + RouteConstants.Offsets[connectingSide.SideName];
        return _cellsGrid.GetCell(nextCellIndex);
    }

    private void TryAttachedCell(Cell to, Cell cellForAttached, ConnectingSide connectingSide)
    {
        var oppositeSide = RouteConstants.OppositeSides[connectingSide.SideName];

        if (cellForAttached.ContainSide(oppositeSide))
        {
            Attach(to, cellForAttached, connectingSide);

            var nextSide = GetUnconnectedSide(cellForAttached);
            var nextCellForAttached = GetNextCellForAttach(nextSide);
            if (nextCellForAttached)
            {
                if (!CheckReadyRoute(nextCellForAttached)) 
                    TryAttachedCell(cellForAttached, nextCellForAttached, nextSide);                    
            }
        }
    }

    private void Attach(Cell to, Cell cell, ConnectingSide connectSide)
    {
        to.GetConnectingSide(connectSide.SideName).Connect();
        cell.GetConnectingSide(RouteConstants.OppositeSides[connectSide.SideName]).Connect();
    }

    private bool CheckReadyRoute(Cell cellForAttached)
    {
        if (cellForAttached == _sourceCell)
        {
            return true;
        }

        return false;
    }
}

public static class RouteConstants
{
    public static readonly Dictionary<SideName, SideName> OppositeSides = new ()
    {
        { SideName.North, SideName.South },
        { SideName.East, SideName.West },
        { SideName.West, SideName.East },
        { SideName.South, SideName.North }
    };

    public static readonly Dictionary<SideName, Vector2Int> Offsets = new ()
    {
        {SideName.North, new Vector2Int(1, 0)},
        {SideName.East, new Vector2Int(0, -1)},
        {SideName.West, new Vector2Int(0, 1)},
        {SideName.South, new Vector2Int(-1, 0)}
    };

    public static readonly List<CellType> ConnectableTypes = new()
    {
        CellType.Source,
        CellType.RoadNe,
        CellType.RoadNs,
        CellType.RoadSe,
        CellType.RoadWe,
        CellType.RoadWn,
        CellType.RoadWs
    };
}

