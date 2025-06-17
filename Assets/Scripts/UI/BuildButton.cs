using Cells;
using GameEvents;
using GroundState;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class BuildButton : MonoBehaviour
    {
        [FormerlySerializedAs("_button")] [SerializeField] private Button button;
        [FormerlySerializedAs("_cellTypeForBuild")] [SerializeField] private CellType cellTypeForBuild;
        private GridStateMachine _gridStateMachine;
        
        private IEventBus _eventBus;

        public void Construct(IEventBus eventBus, GridStateMachine gridStateMachine)
        {
            _eventBus = eventBus;
            _gridStateMachine = gridStateMachine;

            button.onClick.AddListener(OnClick);
        }

        private void OnClick()    
        {
            _eventBus.Invoke(new OnStartBuildingCellEvent(cellTypeForBuild));
            _gridStateMachine.Enter<BuildingState>();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}
