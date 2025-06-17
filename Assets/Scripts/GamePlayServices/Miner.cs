using Cells;
using GameEvents;
using Infrastructure.Services;

namespace GamePlayServices
{
    public class Miner : IMiner
    {
        private readonly IEventBus _eventBus;
        private readonly Cooldown _mineCooldown = new(1);
        private Cell[,] _cells;
        private int _totalCount;
        private int _minePerSecond;


        [Inject]
        public Miner(IEventBus eventBus)
        {
            _eventBus = eventBus;
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

        public void Refresh()
        {
            _minePerSecond = 0;
        
            foreach (Cell cell in _cells)
            {
                if (cell)
                {
                    _minePerSecond += cell.MinePerSecond;
                }
            }
        }
    }
}