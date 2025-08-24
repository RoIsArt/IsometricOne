using System.Collections.Generic;
using Cells;
using GameEvents;
using Infrastructure.Services;
using UnityEditor.Search;

namespace GamePlayServices
{
    public class Miner : IMiner
    {
        private readonly IEventBus _eventBus;
        private readonly Cooldown _mineCooldown = new(1);
        private Cell[,] _cells;
        private int _totalCount;
        private int _minePerSecond;
        private readonly List<Route> _routes;

        [Inject]
        public Miner(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _routes = new List<Route>();
        }
        
        public void Initialize(Cell[,] cells)
        {
            _cells = cells;
        }
    
        public void Mine()
        {
            if (_mineCooldown.IsReady)
            {
                _totalCount += _minePerSecond;
                _eventBus.Invoke(new OnMineValueChanged(_totalCount));
                _mineCooldown.Reset();
            }
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route);
            Refresh();
        }

        private void Refresh()
        {
            _minePerSecond = 0;
            foreach (Route route in _routes)
            {
                foreach (Cell routeCell in route.Cells)
                {
                    _minePerSecond += routeCell.MinePerSecond;
                }
            }
        }
    }
}