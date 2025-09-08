using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class CameraProvider : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}