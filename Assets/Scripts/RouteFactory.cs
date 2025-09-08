using System.Collections.Generic;
using System.Linq;
using Cells;
using GameEvents;
using GamePlayServices;
using Infrastructure.Services;
using UnityEngine;

public class RouteFactory : IRouteFactory
{
    private readonly IMiner _miner;
    private readonly IEventBus _eventBus;

    [Inject]
    public RouteFactory(IMiner miner, IEventBus eventBus)
    {
        _miner = miner;
        _eventBus = eventBus;
    }
        
    public void CreateRoute(List<Cell> route)
    {
        ConnectCells(route);
        Route newRoute = new(route);
        _miner.AddRoute(newRoute);
    }
        
    private void ConnectCells(List<Cell> cells)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            Cell currentCell = cells[i];
            Cell nextCell = cells[(i + 1) % cells.Count];
            currentCell.Connecter.ConnectBidirectional(nextCell);
        }
    }
}