using GameEvents;
using GamePlayServices;
using GroundState;
using Infrastructure.Factories;
using Infrastructure.Game;
using Infrastructure.Services;
using UI;
using UnityEngine;

namespace Infrastructure.SceneManagment
{
    public class GameplaySceneBootstrapper : SceneBootstrapper
    {
        private DiContainer _container;
        private IUIFactory _uiFactory;

        private GroundStateMachine _groundStateMachine;

        public override void ConstructScene(DiContainer container)
        {
            InitializeDependencies(container);
            InitializePrefabs();
        }

        private void InitializeDependencies(DiContainer container)
        {
            _container = container;
            _uiFactory = container.Resolve<IUIFactory>();
            
            _container.StartScope();
            _container.RegisterScene<ICellFactory, CellFactory>(Lifecycle.Singleton);
            _container.RegisterScene<IGridFactory, GridFactory>(Lifecycle.Singleton);
            _container.RegisterScene<IHighlighter, Highlighter>(Lifecycle.Singleton);
            _container.RegisterScene<IBuilder, Builder>(Lifecycle.Singleton);
        }

        private void InitializePrefabs()
        {
            IEventBus eventBus = _container.Resolve<IEventBus>();
            GameObject hud = _uiFactory.CreateHud();
            hud.GetComponent<HUD>().Construct(eventBus);

            _groundStateMachine = new GroundStateMachine(
                eventBus,
                _container.Resolve<IGridFactory>(),
                _container.Resolve<IHighlighter>(),
                _container.Resolve<IBuilder>(),
                _container.Resolve<ICoroutineRunner>());
            
            _groundStateMachine.Enter<InitialState>();
        }

        private void OnDestroy()
        {
            _container.EndScope();
            _groundStateMachine.Dispose();
        }
    }
}