using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _container;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _container = new DIContainer();
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), _container);
        }

        public GameStateMachine StateMachine => _stateMachine;
    }
}