using AssetManagment;
using Infrastructure.Factories;
using Infrastructure.SceneManagment;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;    
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetProvider _assetProvider;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IAssetProvider assetProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);
        
        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            var sceneBootstrapPath = AssetPath.GetSceneBootstrapperPath(SceneManager.GetActiveScene().name);
            var bootstrapObj = _assetProvider.Instantiate(sceneBootstrapPath);
            var sceneBootstrapper = bootstrapObj.GetComponent<SceneBootstrapper>();
            _gameStateMachine.Enter<MainGameState, SceneBootstrapper>(sceneBootstrapper);
        }

    }
}
