using AssetManagment;
using GameEvents;
using Infrastructure.Factories;
using Infrastructure.Game;
using Infrastructure.SceneManagment;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly DiContainer _container;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, DiContainer container,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _container = container;
            _coroutineRunner = coroutineRunner;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("GameplayScene");

        private void RegisterServices()
        {
            _container.RegisterInstance<ICoroutineRunner>(_coroutineRunner);
            _container.RegisterGlobal<IAssetProvider, AssetProvider>(Lifecycle.Singleton);
            _container.RegisterGlobal<IUIFactory, UIFactory>(Lifecycle.Singleton);
            _container.RegisterGlobal<ISceneLoader, SceneLoader>(Lifecycle.Singleton);
            _container.RegisterGlobal<IStaticDataService, StaticDataService>(Lifecycle.Singleton);
            _container.RegisterGlobal<IEventBus, EventBus>(Lifecycle.Singleton);
        }
    }
}
