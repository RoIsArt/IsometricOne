using GameEvents;
using GroundState;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class HUD :MonoBehaviour
    {
        [SerializeField] private BuilderPanel builderPanel;
        [SerializeField] private MinePanel minePanel;
        
        public void Construct(IEventBus eventBus, GridStateMachine gridStateMachine)
        {
            builderPanel.Construct(eventBus, gridStateMachine);
            minePanel.Construct(eventBus);
        }
    }
}