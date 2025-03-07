using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ServiceLoader : MonoBehaviour
{
    [SerializeField] private GroundStateMachine _groundStateMachine;
    [SerializeField] private CellsGrid _cellsGrid;

    private EventBus _eventBus;
    private CellFactory _cellFactory;
    private Pointer _pointer;
    private Highlighter _highlighter;
    private Builder _builder;

    private List<IDisposable> _disposables;

    private void Awake()
    {
        _eventBus = new EventBus();
        _cellFactory = new CellFactory();
        _pointer = new Pointer();
        _highlighter = new Highlighter();
        _builder = new Builder();

        _disposables = new List<IDisposable>();
    }

    private void OnDestroy()
    {
        if (_disposables.Count > 0)
        {
            foreach (var service in _disposables)
            {
                service.Dispose();
            }
        }
    }

    public void RegisterServices()
    {
        ServiceLocator.Instance.Register(_groundStateMachine);
        ServiceLocator.Instance.Register(_cellsGrid);
        ServiceLocator.Instance.Register(_eventBus);
        ServiceLocator.Instance.Register(_cellFactory);
        ServiceLocator.Instance.Register(_pointer);
        ServiceLocator.Instance.Register(_highlighter);
        ServiceLocator.Instance.Register(_builder);
    }

    public void InitializeServices()
    {
        _cellsGrid.Init();
        _pointer.Init();
        _highlighter.Init();
        _builder.Init();
        _cellFactory.Init();
        _groundStateMachine.Init();
    }

    public void AddDisposables()
    {
        _disposables.Add(_highlighter);
    }
}
