using Cells;
using UnityEngine;

namespace GamePlayServices
{
    [RequireComponent(typeof(Cell))]
    public class MouseObserver : MonoBehaviour
    {
        private Cell _cell;

        public void Awake() => 
            _cell = gameObject.GetComponent<Cell>();

        private void OnMouseEnter() => 
            _cell.Point();

        private void OnMouseExit() => 
            _cell.Point();
    }
}
