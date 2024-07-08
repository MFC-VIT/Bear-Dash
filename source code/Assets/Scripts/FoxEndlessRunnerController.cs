using UnityEngine;
using UnityEngine.SceneManagement; // Include the SceneManager

public class FoxEndlessRunnerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float strafeSpeed = 5f;
    public float jumpForce = 6f;
    public float jumpCooldown = 3f;
    public Transform mainCamera;
    public float cameraFollowSpeed = 5f;
    public float cameraDistance = 5f;
    public float cameraHeight = 3f;

    private Rigidbody rb;
    private bool canJump = true;
    private float jumpTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // To prevent the fox from rotating when hitting objects
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

        // Update camera position
        UpdateCameraPosition();
    }

    private void FixedUpdate()
    {
        // Move the fox forward continuously
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;

        // Handle horizontal movement input
        float horizontalInput = Input.GetAxis("Horizontal") * strafeSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput;
        Vector3 movement = forwardMove + horizontalMove;

        rb.MovePosition(rb.position + movement);
    }

    private void UpdateCameraPosition()
    {
        // Calculate the desired camera position
        Vector3 targetCameraPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;

        // Smoothly move the camera towards the desired position
        mainCamera.position = Vector3.Lerp(mainCamera.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Load the scene with index 0
            SceneManager.LoadScene(0);
        }
    }
}
