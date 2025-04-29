using System;

namespace Assets.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;    
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _UIFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIFactory UIFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _UIFactory = UIFactory;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);
        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _UIFactory.CreateHud();

            _gameStateMachine.Enter<GameLoopState>();
        }

    }
}
