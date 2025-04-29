using Assets.Scripts.GameEvents;
using System;

public class MiningState : IGroundState, IDisposable
{
    private readonly Highlighter _highlighter;
    private readonly Miner _miner;
    private readonly EventBus _eventBus;

    public MiningState(Highlighter highlighter, EventBus eventBus)
    {
        _highlighter = highlighter;
        _eventBus = eventBus;
    }

    private void StartMine(OnRouteIsReady onRouteIsReady)
    {
        _miner.SetRoute(onRouteIsReady.Route);
    }

    public void Enter()
    {
        Action<Cell> action = _highlighter.HighlightForMine;
        _highlighter.SetHighlightMethod(action);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }


    public void Exit()
    {

    }

    public void Dispose()
    {
        //_eventBus.Unsubscribe<OnRouteIsReady>(StartMine);
    } 
}
