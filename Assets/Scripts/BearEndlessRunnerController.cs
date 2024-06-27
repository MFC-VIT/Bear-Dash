using UnityEngine;

namespace BLINK
{
    public class BearEndlessRunnerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 6f;
        public float horizontalSpeed = 5f; 
        public Transform mainCamera;
        public float cameraFollowSpeed = 5f;
        public float cameraDistance = 5f;
        public float cameraHeight = 3f;

        private Rigidbody rb;
        private bool canJump = true;
        private float jumpCooldown = 1f; // Cooldown time in seconds
        private float jumpTimer = 0f;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; // To prevent the bear from rotating when hitting objects
        }

        private void Update()
        {
            // Update jump cooldown timer
            if (!canJump)
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer >= jumpCooldown)
                {
                    canJump = true;
                    jumpTimer = 0f;
                }
            }

            // Handle input for jumping
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false; // Disable jumping until cooldown is over
            }
        }

        private void FixedUpdate()
        {
            // Move the bear forward continuously
            Vector3 forwardMove = transform.forward * moveSpeed;

            // Handle horizontal movement input
            float horizontalInput = Input.GetAxis("Horizontal") * horizontalSpeed;
            Vector3 horizontalMove = transform.right * horizontalInput;
            Vector3 movement = forwardMove + horizontalMove;

            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

            // Update camera position
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            // Calculate the desired camera position
            Vector3 targetCameraPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;

            // Smoothly move the camera towards the desired position
            mainCamera.position = Vector3.Lerp(mainCamera.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }
}
