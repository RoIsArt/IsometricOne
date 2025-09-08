using System;
using System.Collections.Generic;
using System.Linq;
using Cells;
using GameEvents;
using Infrastructure.Services;
using UnityEngine;

public class RouteConstructor : IRouteConstructor, IDisposable
{
    private readonly IEventBus _eventBus;
    private readonly IRouteFactory _routeFactory;
    private CellsGrid _cellsGrid;

    [Inject]
    public RouteConstructor(IEventBus eventBus, IRouteFactory routeFactory)
    {
        _eventBus = eventBus;
        _routeFactory = routeFactory;

        _eventBus.Subscribe<OnCellsGridCreatedEvent>(SetGrid);
        _eventBus.Subscribe<OnCellBuildedEvent>(CheckConnection);
    }

    private void SetGrid(OnCellsGridCreatedEvent onCellsGridCreatedEvent) => 
        _cellsGrid = onCellsGridCreatedEvent.CellsGrid;

    public void CheckConnection(OnCellBuildedEvent onCellBuildedEvent)
    {
        if(!onCellBuildedEvent.CellType.IsConnectable())
            return;

        List<SideName> sides = _cellsGrid.SourceCell.Connecter.Sides.Keys.ToList();
        foreach (SideName side in sides)
        {
            if(_cellsGrid.SourceCell.Connecter.Sides[side] != null) 
                continue;
            
            List<Cell> route = new List<Cell>();
            bool routeIsReady = CheckRouteReady(route, _cellsGrid.SourceCell, side);
            if (!routeIsReady) continue;
            _routeFactory.CreateRoute(route);
        }
    }

    private bool CheckRouteReady(List<Cell> route, Cell currentCell, SideName unconnectedSide)
    {
        if (route.Count > 1 && route[0] == currentCell)
            return true;
        
        route.Add(currentCell);
        
        Vector2Int index = currentCell.Index + unconnectedSide.ToDirection();
        Cell nextCell = _cellsGrid.GetCell(index);
        if (IsCellMayConnect(unconnectedSide, nextCell, out SideName oppositeSide))
        {
            SideName nextSide = FindNextSide(nextCell, oppositeSide);
            if (nextSide != SideName.None)
                return CheckRouteReady(route, nextCell, nextSide);
        }
        
        return false;
    }

    private SideName FindNextSide(Cell nextCell, SideName oppositeSide)
    {
        foreach (SideName sideName in nextCell.Connecter.Sides.Keys)
        {
            if (sideName != oppositeSide && nextCell.Connecter.Sides[sideName] == null)
                return sideName;
        }

        return SideName.None;
    }

    private bool IsCellMayConnect(SideName unconnectedSide, Cell nextCell, out SideName oppositeSide)
    {
        oppositeSide = SideName.None;
        return nextCell && 
               nextCell.Type.IsConnectable() && 
               CheckOppositeSideInCell(nextCell, unconnectedSide, out oppositeSide);
    }

    private bool CheckOppositeSideInCell(Cell nextCell, SideName unconnectedSide, out SideName oppositeSide)
    {
        oppositeSide = unconnectedSide.GetOpposite();
        return nextCell.Connecter.ContainSide(oppositeSide);
    }
    
    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellsGridCreatedEvent>(SetGrid);
        _eventBus.Unsubscribe<OnCellBuildedEvent>(CheckConnection);
    }
}