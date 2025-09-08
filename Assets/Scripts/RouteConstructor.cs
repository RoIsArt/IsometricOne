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
        
        
        foreach (KeyValuePair<SideName, Cell> side in _cellsGrid.SourceCell.Connecter.Sides)
        {
            if(side.Value != null) 
                continue;
            
            List<Cell> route = new List<Cell>();
            bool routeIsReady = CheckRouteReady(route, _cellsGrid.SourceCell, side.Key);
            if (!routeIsReady) continue;
            route.Reverse();
            _routeFactory.CreateRoute(route);
        }
    }

    private bool CheckRouteReady(List<Cell> route, Cell currentCell, SideName unconnectedSide)
    {
        if (route.Count > 1 && route.First() == currentCell)
            return true;
        else
            route.Add(currentCell);

        
        Vector2Int index = currentCell.Index + unconnectedSide.ToDirection();
        Cell nextCell = _cellsGrid.GetCell(index);
        if (nextCell && nextCell.Type.IsConnectable()
                     && CheckOppositeSideInCell(nextCell, unconnectedSide, out SideName oppositeSide))
        {

            SideName nextSide = nextCell.Connecter.Sides.Keys.FirstOrDefault(side => side != oppositeSide);
            return CheckRouteReady(route, nextCell, nextSide);
        }
        else
            return false;
    }

    private bool CheckOppositeSideInCell(Cell nextCell, SideName unconnectedSide, out SideName oppositeSide)
    {
        bool contain = nextCell.Connecter.ContainSide(unconnectedSide.GetOpposite());
        if (contain)
        {
            oppositeSide = unconnectedSide.GetOpposite();
            return true;
        }
        else
        {
            oppositeSide = SideName.None;
            return false;
        }
    }
    
    public void Dispose()
    {
        _eventBus.Unsubscribe<OnCellsGridCreatedEvent>(SetGrid);
        _eventBus.Unsubscribe<OnCellBuildedEvent>(CheckConnection);
    }
}