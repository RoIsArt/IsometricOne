using GameEvents;
using GroundState;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class HUD :MonoBehaviour
    {
        [FormerlySerializedAs("_builderPanel")] [SerializeField] private BuilderPanel builderPanel;
        
        public void Construct(IEventBus eventBus, GridStateMachine gridStateMachine)
        {
            builderPanel.Construct(eventBus, gridStateMachine);
        }
    }
}