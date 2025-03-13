using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour, IDisposable
{
    [SerializeField] private Button _button;
    [SerializeField] private CellData _cellForBuild;

    private BuildingState _buildingState;
    private EventBus _eventBus;

    [Inject]
    public void Construct(EventBus eventBus, BuildingState buildingState)
    {
        _eventBus = eventBus;
        _buildingState = buildingState;

        _button.onClick.AddListener(StartBuilding);
    }

    public void StartBuilding()    
    {
        _eventBus.Invoke(new OnStartBuildingCellEvent(_cellForBuild));
        _eventBus.Invoke(new OnChangeGroundStateEvent(_buildingState));   
    }

    public void Dispose()
    {
        _button.onClick.RemoveListener(StartBuilding);
    }
}
