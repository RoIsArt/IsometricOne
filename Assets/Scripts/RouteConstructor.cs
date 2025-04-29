using Assets.Scripts.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteConstructor : IDisposable
{
    private readonly Route _route;
    private readonly CellsGrid _cellsGrid;
    private readonly EventBus _eventBus;

    private readonly Dictionary<Side, Side> _oppositeSide = new ()
        {
            { Side.NORTH, Side.SOUTH },
            { Side.EAST, Side.WEST },
            { Side.WEST, Side.EAST },
            { Side.SOUTH, Side.NORTH }
        };
    private readonly Dictionary<Side, Vector2Int> _offsetToSide = new ()
        {
            {Side.NORTH, new Vector2Int(1, 0)},
            {Side.EAST, new Vector2Int(0, -1)},
            {Side.WEST, new Vector2Int(0, 1)},
            {Side.SOUTH, new Vector2Int(-1, 0)}
        };

    public RouteConstructor(CellsGrid cellsGrid, Route route, EventBus eventBus)
    {
        _eventBus = eventBus;
        _cellsGrid = cellsGrid;
        _route = route;

        _eventBus.Subscribe<OnCellBuildedEvent>(AddInRoute);
    }

    public ConnectingCell LastCellInRoute { get { return _route.Last; } }

    public void AddInRoute(OnCellBuildedEvent onCellBuildedEvent)
    {
        if (_route.IsReady || onCellBuildedEvent.Cell.Data.Type != CellType.ROAD) return;

        var unconnectedSide = GetUnconnectedSide(LastCellInRoute);
        var cellForAttached = GetNextCellForAttach(unconnectedSide);

        if(cellForAttached is ConnectingCell cell)
            CheckNextCell(cell, unconnectedSide);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellBuildedEvent>(AddInRoute);
    }

    private ConnectingSide GetUnconnectedSide(ConnectingCell cell)
    {
        foreach (var side in cell.ConnectingSides)
        {
            if(!side.IsConnected) 
                return side; 
        }

        return null;
    }

    private Cell GetNextCellForAttach(ConnectingSide connectingSide)
    {
        var nextCellIndex = LastCellInRoute.Index + _offsetToSide[connectingSide.Side];
        return _cellsGrid[nextCellIndex];
    }

    private void CheckNextCell(ConnectingCell cellForAttached, ConnectingSide connectingSide)
    {
        if (cellForAttached == null || cellForAttached.Data.Type != CellType.ROAD) return;

        var oppositeSide = _oppositeSide[connectingSide.Side];

        if (cellForAttached.ContainSide(oppositeSide))
        {
            Attach(LastCellInRoute, cellForAttached, connectingSide);

            var nextSide = GetUnconnectedSide(cellForAttached);
            var nextCellForAttached = GetNextCellForAttach(nextSide);
            if (nextCellForAttached is ConnectingCell cell)
            {
                if (!CheckReadyRoute(cell)) CheckNextCell(cell, nextSide);                    
            }
        }
    }

    private void Attach(ConnectingCell to, ConnectingCell cell, ConnectingSide connectSide)
    {
        to.GetConnectingSide(connectSide.Side).Connected();
        cell.GetConnectingSide(_oppositeSide[connectSide.Side]).Connected();
        _route.Add(cell);
    }

    private bool CheckReadyRoute(ConnectingCell cellForAttached)
    {
        if (cellForAttached == _route.First)
        {
            _eventBus.Invoke(new OnRouteIsReady(_route));
            return true;
        }

        return false;
    }
}

