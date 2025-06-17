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

        private Vector3 _targetPosition;
        private Vector3 _currentVelocity; 

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
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
            _targetPosition.x = Mathf.Clamp(_targetPosition.x, minBounds.x, maxBounds.x);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, minBounds.y, maxBounds.y);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                new Vector3((minBounds.x + maxBounds.x) * 0.5f, (minBounds.y + maxBounds.y) * 0.5f, 0f),
                new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 1f)
            );
        }
    }
}