using System;
using System.Collections.Generic;
using AssetManagment;
using Infrastructure.Game;
using Infrastructure.SceneManagment;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, DiContainer container, ICoroutineRunner coroutineRunner) 
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container, coroutineRunner),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, container.Resolve<IAssetProvider>()),
                [typeof(MainGameState)] = new MainGameState(container)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}
