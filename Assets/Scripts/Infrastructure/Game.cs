using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ServiceLocator.Container);
        }

        public GameStateMachine StateMachine => _stateMachine;
    }
}