using Infrastructure.GameStates;
using Infrastructure.SceneManagment;
using Infrastructure.Services;

namespace Infrastructure.Game
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, DiContainer container)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), container, coroutineRunner);
        }

        public GameStateMachine StateMachine => _stateMachine;
    }
}