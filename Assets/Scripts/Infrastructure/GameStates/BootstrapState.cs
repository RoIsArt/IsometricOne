using Assets.Scripts.AssetManagment;
using Assets.Scripts.Infrastructure.Services;
using System;
using System.ComponentModel;

namespace Assets.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string INITIAL = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private ServiceLocator _container;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator container)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _container = container;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("MainScene");

        private void RegisterServices()
        {
            _container.RegisterSingle<IAssets>(new AssetProvider());
            _container.RegisterSingle<IUIFactory>(new UIFactory(Services.ServiceLocator.Container.Single<IAssets>()));
        }
    }
}
