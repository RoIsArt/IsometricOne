namespace Assets.Scripts.Infrastructure
{
    public class GameLoopState : IState
    {
        private GameStateMachine gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
