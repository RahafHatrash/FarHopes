using UnityEngine;

public class HalasMover : MonoBehaviour
{
    public float baseSpeed = 5f;
    public int score = 0;
    public float jumpForce = 3f;

    private Rigidbody2D rb;
    private float currentSpeed;

    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip landClip;

    private bool wasGrounded = true;
    private float walkTimer = 0f;
    private float walkCooldown = 0.3f;

    [Header("Ground Detection")]
    public Transform groundCheck;            // Empty GameObject at player's feet
    public float groundCheckRadius = 0.1f;   // Small circle size
    public LayerMask groundLayer;            // Assign your ground layer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Move right constantly
        currentSpeed = baseSpeed + (score * 3);
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

        // Handle Jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Check grounded status
        bool isGrounded = IsGrounded();

        // Play landing sound once when touching ground
        if (!wasGrounded && isGrounded)
        {
            audioSource.PlayOneShot(landClip);
        }

        // Play walk sound while grounded
        if (isGrounded)
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0f)
            {
                audioSource.PlayOneShot(walkClip);
                walkTimer = walkCooldown;
            }
        }

        wasGrounded = isGrounded;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


    public void IncreaseScore()
    {
        score++;
    }

    // Optional: visualize ground check circle in Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
