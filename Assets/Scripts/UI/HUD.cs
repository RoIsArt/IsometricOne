using System.Collections.Generic;
using GameEvents;
using UnityEngine;

namespace UI
{
    public class HUD :MonoBehaviour
    {
        [SerializeField] private List<BuildButton> _buildButtons;

        public void Construct(IEventBus eventBus)
        {
            foreach (var buildButton in _buildButtons)
            {
                buildButton.Construct(eventBus);
            }
        }
    }
}