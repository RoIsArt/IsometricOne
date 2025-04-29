using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour, IDisposable
{
    [SerializeField] private Button _button;
    [SerializeField] private string _cellKeyForBuild;

    private BuildingState _buildingState;
    private EventBus _eventBus;

    public void Construct(EventBus eventBus, BuildingState buildingState)
    {
        _eventBus = eventBus;
        _buildingState = buildingState;

        _button.onClick.AddListener(StartBuilding);
    }

    public void StartBuilding()    
    {
        //_eventBus.Invoke(new OnStartBuildingCellEvent(_cellKeyForBuild));
        _eventBus.Invoke(new OnChangeGroundStateEvent(_buildingState));   
    }

    public void Dispose()
    {
        _button.onClick.RemoveListener(StartBuilding);
    }
}
