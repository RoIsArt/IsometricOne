using Infrastructure.SceneManagment;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class MainGameState : IPayloadedState<SceneBootstrapper>
    {
        private readonly DiContainer _container;
        private SceneBootstrapper _sceneBootstrapper;

        public MainGameState(DiContainer container)
        {
            _container = container;
        }

        public void Enter(SceneBootstrapper sceneBootstrapper)
        {
            sceneBootstrapper.ConstructScene(_container);
        }

        public void Exit()
        {

        }

    }
}