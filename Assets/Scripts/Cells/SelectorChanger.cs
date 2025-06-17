using UnityEngine;

namespace Cells
{
    public class SelectorChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _selector;

        public void SetActive(bool activity) => 
            _selector.SetActive(activity);
    }
}