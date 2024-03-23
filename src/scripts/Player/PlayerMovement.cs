using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public float discForce = 10.0f; // Force of the disc
    public int maxDiscs = 3; // Maximum number of discs available
    public float discCooldown = 4.0f; // Time until the player can use a disc again
    public float discLifetime = 2.0f; // Lifetime of the disc

    public Rigidbody rb;
    public Transform cameraTransform;

    // Cooldown and disc count variables
    private float currentCooldownTime;
    private int currentDiscCount;

    // Variable to hold the disc prefab
    public GameObject discPrefab;

    // Variable to hold the movement velocity
    private Vector3 movementVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        currentDiscCount = maxDiscs;

        // Load the disc prefab if it has been assigned in the inspector
        if (discPrefab == null)
        {
            discPrefab = Resources.Load<GameObject>("Disc");
        }
    }

    private void Update()
    {
        if (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
        }

        if (currentDiscCount > 0 && Input.GetMouseButtonDown(0) && currentCooldownTime <= 0)
        {
            // Create a disc at the player's position
            Vector3 discPosition = transform.position + cameraTransform.forward * 1.5f;
            GameObject disc = Instantiate(discPrefab, discPosition, Quaternion.identity);

            // Add force to the disc
            disc.GetComponent<Rigidbody>().AddForce(cameraTransform.forward * discForce, ForceMode.Impulse);

            // Destroy the disc after a set amount of time
            Destroy(disc, discLifetime);

            // Decrease disc count and set cooldown time
            currentDiscCount--;
            currentCooldownTime = discCooldown;
        }

        // Calculate the movement velocity
        movementVelocity = cameraTransform.forward * Input.GetAxis("Vertical") * speed;
        movementVelocity += cameraTransform.right * Input.GetAxis("Horizontal") * speed;
        movementVelocity = Vector3.ClampMagnitude(movementVelocity, speed);

        // Add the movement velocity to the rigidbody
        rb.velocity = movementVelocity;

        // Add jump force if the jump key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}