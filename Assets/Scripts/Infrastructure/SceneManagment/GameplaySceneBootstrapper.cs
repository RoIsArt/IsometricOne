using AssetManagment;
using Cells;
using GameEvents;
using GamePlayServices;
using GroundState;
using Infrastructure.Factories;
using Infrastructure.Services;
using UI;
using UnityEngine;

namespace Infrastructure.SceneManagment
{
    public class GameplaySceneBootstrapper : SceneBootstrapper
    {
        private DiContainer _container;
        private IUIFactory _uiFactory;
        private IEventBus _eventBus;

        public override void ConstructScene(DiContainer container)
        {
            InitializeDependencies(container);
            InitGameWorld();
        }

        private void InitializeDependencies(DiContainer container)
        {
            _container = container;
            _uiFactory = container.Resolve<IUIFactory>();
            _eventBus = container.Resolve<IEventBus>();
            
            _container.StartScope();
            _container.RegisterScene<ICellFactory, CellFactory>(Lifecycle.Singleton);
            _container.RegisterScene<IGridFactory, GridFactory>(Lifecycle.Singleton);
            _container.RegisterScene<IPointer, Pointer>(Lifecycle.Singleton);
            _container.RegisterScene<IHighlighter, Highlighter>(Lifecycle.Singleton);
            _container.RegisterScene<IBuilder, Builder>(Lifecycle.Singleton);
            _container.RegisterScene<IMiner, Miner>(Lifecycle.Singleton);
        }

        private void InitGameWorld()
        {
            GameObject grid = _container.Resolve<IGridFactory>().Create();
            GridStateMachine gridStateMachine = new GridStateMachine(
                grid.GetComponent<CellsGrid>(),
                _container.Resolve<IHighlighter>(),
                _container.Resolve<IMiner>(),
                _container.Resolve<IBuilder>());
            
            GameObject hud = _uiFactory.CreateHud();
            hud.GetComponent<HUD>().Construct(_eventBus, gridStateMachine);
            
            gridStateMachine.Enter<InitialState>();
        }

        private void OnDestroy()
        {
            _container.EndScope();
        }
    }
}