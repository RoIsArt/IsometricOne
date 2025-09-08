using UnityEngine;

namespace CameraManagment
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float smoothingFactor = 5f; 

        [Header("Camera Bounds")]
        [SerializeField] private Vector2 minBounds;
        [SerializeField] private Vector2 maxBounds;
        
        private Camera _camera;
        private Vector3 _targetPosition;
        private Vector3 _currentVelocity; 
        private float _zoomVelocity;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }
        
        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void LateUpdate()
        {
            HandleInput();
            ApplyMovement();
            ClampPosition();
        }

        private void HandleInput()
        {
            Vector2 input = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
            
            if (input.magnitude > 1f)
            {
                input.Normalize();
            }

            _targetPosition += (Vector3)input * (moveSpeed * Time.deltaTime);
        }

        private void ApplyMovement()
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                _targetPosition,
                ref _currentVelocity,
                1f / smoothingFactor
            );
        }

        private void ClampPosition()
        {
            float cameraHeight = _camera.orthographicSize;
            float cameraWidth = cameraHeight * _camera.aspect;
            
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, minBounds.x + cameraWidth, maxBounds.x - cameraWidth);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, minBounds.y + cameraHeight, maxBounds.y - cameraHeight);
        }
        
    }
}