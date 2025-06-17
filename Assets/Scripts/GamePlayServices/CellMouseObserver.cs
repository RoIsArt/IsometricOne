using System;
using Cells;
using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlayServices
{
    [RequireComponent(typeof(Cell))]
    public class CellMouseObserver : MonoBehaviour
    {
        private IEventBus _eventBus;
        private Cell _cell;

        public void Construct(IEventBus eventBus, Cell cell)
        {
            _eventBus = eventBus;
            _cell = cell;
        }

        private void OnMouseEnter()
        {
            if(!IsMouseOnUI())
                _eventBus.Invoke(new OnCellMouseEnterEvent(_cell));
        }

        private void OnMouseExit()
        {
            if(!IsMouseOnUI())
                _eventBus.Invoke(new OnCellMouseExitEvent(_cell));
        }

        private bool IsMouseOnUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _eventBus.Invoke(new OnCellMouseExitEvent(_cell));
                return true;
            }

            return false;
        }
    }
}
