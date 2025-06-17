using Cells;

namespace GroundState
{
    public class MiningState : IUpdatableGridState
    {
        private readonly Cell[,] _cells;
        private readonly IMiner _miner;

        public MiningState(Cell[,] cells, IMiner miner)
        {
            _cells = cells;
            _miner = miner;
        }
        
        public void Enter()
        {
            _miner.Initialize(_cells);
            _miner.Refresh();
        }

        public void Update()
        {
            _miner.Mine();
        }
        
        public void Exit()
        {
            
        }

    }
}