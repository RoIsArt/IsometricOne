using Cells;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CellType _cellTypeForBuild;
        
        private IEventBus _eventBus;

        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()    
        {
            _eventBus.Invoke<OnStartBuildingCellEvent>(new OnStartBuildingCellEvent(_cellTypeForBuild));
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}
