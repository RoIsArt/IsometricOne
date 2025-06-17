using Cells;
using GamePlayServices;

namespace GroundState
{
    public class MiningState : IUpdatableGridState
    {
        private readonly Cell[,] _cells;
        private readonly IMiner _miner;
        private readonly IHighlighter _highlighter;

        public MiningState(Cell[,] cells, IMiner miner, IHighlighter highlighter)
        {
            _cells = cells;
            _miner = miner;
            _highlighter = highlighter;
        }
        
        public void Enter()
        {
            _miner.Initialize(_cells);
            _miner.Refresh();
        }

        public void Update()
        {
            _highlighter.Highlight();
            _miner.Mine();
        }
        
        public void Exit()
        {
            
        }

    }
}