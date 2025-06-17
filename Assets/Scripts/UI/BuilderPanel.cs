using System.Collections.Generic;
using GameEvents;
using GroundState;
using UnityEngine;

namespace UI
{
    public class BuilderPanel : MonoBehaviour
    {
        [SerializeField] private List<BuildButton> buildButtons;

        public void Construct(IEventBus eventBus, GridStateMachine gridStateMachine)
        {
            foreach (BuildButton buildButton in buildButtons)
            {
                buildButton.Construct(eventBus, gridStateMachine);
            }
        }
    }
}