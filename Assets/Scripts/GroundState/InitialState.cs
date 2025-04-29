namespace Assets.Scripts.GroundState
{
    public class InitialState : IGroundState
    {
        private GroundStateMachine _groundStateMachine;
        private CellsGrid _cellsGrid;
        private GridFactory _gridGenerator;
        private CellFactory _cellFactory;
        private CellsDataConfig _cellsDataConfig;
        private GridConfig _cellsGridConfig;

        public InitialState(CellsGrid cellsGrid, 
                            GridFactory gridGenerator, 
                            CellFactory cellFactory, 
                            CellsDataConfig cellsDataConfig, 
                            GridConfig cellsGridConfig)
        {
            _cellsGrid = cellsGrid;
            _gridGenerator = gridGenerator;
            _cellFactory = cellFactory;
            _cellsDataConfig = cellsDataConfig;
            _cellsGridConfig = cellsGridConfig;
        }

        public void Enter()
        {
            _gridGenerator.Create();
            _groundStateMachine.SetState<MiningState>();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
