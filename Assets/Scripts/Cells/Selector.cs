using UnityEngine;
using UnityEngine.Serialization;

namespace Cells
{
    public class Selector : MonoBehaviour
    {
        [FormerlySerializedAs("_selector")] 
        [SerializeField] private GameObject selector;

        public void Show() => SetActive(true);
        public void Hide() => SetActive(false);
        public void Toggle() => SetActive(!selector.activeSelf);
        
        private void SetActive(bool activity) => 
            selector.SetActive(activity);
    }
}