using Infrastructure.GameStates;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Game
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        private DiContainer _container;

        private void Awake()
        {
            _container = new DiContainer();
            _game = new Game(this, _container);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            _container.Dispose();
        }
    }
}
