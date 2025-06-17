using UnityEngine;

namespace CameraManagment
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        [Header("Camera Bounds")]
        [SerializeField] private Vector2 minBounds;
        [SerializeField] private Vector2 maxBounds;
        

        private void Update()
        {
            Vector3 moveDirection = Vector3.zero;
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical"); 
            
            if (horizontal > 0.9) moveDirection.x += horizontal;
            else if (horizontal < 0.9) moveDirection.x += horizontal;

            if (vertical > 0.9) moveDirection.y += vertical;
            else if (vertical < 0.9) moveDirection.y += vertical;
            
            if (moveDirection.magnitude > 0) moveDirection.Normalize();
            
            Vector3 newPosition = transform.position + moveDirection * (moveSpeed * Time.deltaTime);
            
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

            transform.position = newPosition;
        }
    }
}
