using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour, IDisposable
{
    [SerializeField] private Button _button;
    [SerializeField] private CellData _cellForBuild;

    private EventBus _eventBus;
    private BuildingState _buildingState;

    [Inject]
    public void Construct(EventBus eventBus, BuildingState buildingState)
    {
        _eventBus = eventBus;
        _buildingState = buildingState;

        _button.onClick.AddListener(StartBuilding);
    }

    public void StartBuilding()    
    {
        _eventBus.Invoke<OnStartBuildingCellEvent>(new OnStartBuildingCellEvent(_cellForBuild));
        _eventBus.Invoke<OnChangeGroundStateEvent>(new OnChangeGroundStateEvent(_buildingState));   
    }

    public void Dispose()
    {
        _button.onClick.RemoveListener(StartBuilding);
    }
}
