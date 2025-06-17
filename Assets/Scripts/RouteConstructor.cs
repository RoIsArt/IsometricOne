using System;
using System.Collections.Generic;
using Cells;
using GameEvents;
using UnityEngine;

public class RouteConstructor : IDisposable
{
    private readonly Route _route;
    private readonly CellsGrid _cellsGrid;
    private readonly EventBus _eventBus;

    private readonly Dictionary<SideName, SideName> _oppositeSide = new ()
    {
        { SideName.North, SideName.South },
        { SideName.East, SideName.West },
        { SideName.West, SideName.East },
        { SideName.South, SideName.North }
    };
    private readonly Dictionary<SideName, Vector2Int> _offsetToSide = new ()
    {
        {SideName.North, new Vector2Int(1, 0)},
        {SideName.East, new Vector2Int(0, -1)},
        {SideName.West, new Vector2Int(0, 1)},
        {SideName.South, new Vector2Int(-1, 0)}
    };
    private readonly List<CellType> _connectableTypes = new()
    {
        CellType.Source,
        CellType.RoadNe,
        CellType.RoadNs,
        CellType.RoadSe,
        CellType.RoadWe,
        CellType.RoadWn,
        CellType.RoadWs
    };

    public RouteConstructor(CellsGrid cellsGrid, Route route, EventBus eventBus)
    {
        _eventBus = eventBus;
        _cellsGrid = cellsGrid;
        _route = route;

        _eventBus.Subscribe<OnCellBuildedEvent>(AddInRoute);
    }

    public Cell LastCellInRoute { get { return _route.Last; } }

    public void AddInRoute(OnCellBuildedEvent onCellBuildedEvent)
    {
        if (_route.IsReady || _connectableTypes.Contains(onCellBuildedEvent.Cell.Type)) return;

        var unconnectedSide = GetUnconnectedSide(LastCellInRoute);
        var cellForAttached = GetNextCellForAttach(unconnectedSide);

        if(cellForAttached)
            CheckNextCell(cellForAttached, unconnectedSide);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellBuildedEvent>(AddInRoute);
    }

    private ConnectingSide GetUnconnectedSide(Cell cell)
    {
        return cell.GetUnconnetSide();
    }

    private Cell GetNextCellForAttach(ConnectingSide connectingSide)
    {
        var nextCellIndex = LastCellInRoute.Index + _offsetToSide[connectingSide.SideName];
        return _cellsGrid.GetCell(nextCellIndex);
    }

    private void CheckNextCell(Cell cellForAttached, ConnectingSide connectingSide)
    {
        if (cellForAttached == null || !_connectableTypes.Contains(cellForAttached.Type)) return;

        var oppositeSide = _oppositeSide[connectingSide.SideName];

        if (cellForAttached.ContainSide(oppositeSide))
        {
            Attach(LastCellInRoute, cellForAttached, connectingSide);

            var nextSide = GetUnconnectedSide(cellForAttached);
            var nextCellForAttached = GetNextCellForAttach(nextSide);
            if (nextCellForAttached)
            {
                if (!CheckReadyRoute(nextCellForAttached)) CheckNextCell(nextCellForAttached, nextSide);                    
            }
        }
    }

    private void Attach(Cell to, Cell cell, ConnectingSide connectSide)
    {
        to.GetConnectingSide(connectSide.SideName).Connect();
        cell.GetConnectingSide(_oppositeSide[connectSide.SideName]).Connect();
        _route.Add(cell);
    }

    private bool CheckReadyRoute(Cell cellForAttached)
    {
        if (cellForAttached == _route.First)
        {
            _eventBus.Invoke(new OnRouteIsReady(_route));
            return true;
        }

        return false;
    }
}

