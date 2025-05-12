using UnityEngine;

namespace UI
{
    public class CameraProvider : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }
    }
}