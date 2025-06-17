using GameEvents;
using GamePlayServices;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class MinePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI value;

        private IEventBus _eventBus;

        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            
            _eventBus.Subscribe<OnMineValueChanged>(ChangeValue);
        }

        private void ChangeValue(OnMineValueChanged onMineValueChanged)
        {
            value.text = onMineValueChanged.Value.ToString();
        }
    }
}
