using Assets.Scripts.Infrastructure;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private Game _game;

    private void Awake()
    {
        _game = new Game(this);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
