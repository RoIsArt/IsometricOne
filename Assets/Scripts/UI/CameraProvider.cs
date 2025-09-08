using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class CameraProvider : MonoBehaviour
    {
        [FormerlySerializedAs("_canvas")] [SerializeField] private Canvas canvas;

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}