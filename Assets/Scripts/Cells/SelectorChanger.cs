using UnityEngine;

namespace Cells
{
    public class SelectorChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _selector;

        public void Activate()
        {
            if(!_selector.activeSelf)
                _selector.SetActive(true);
        }

        public void Deactivate()
        {
            if(_selector.activeSelf)
                _selector.SetActive(false);
        }
    }
}