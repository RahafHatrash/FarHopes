using UnityEngine;

public class MouseMover : MonoBehaviour
{
    [Header("Rats")]
    public Rigidbody2D topRatRB;
    public Rigidbody2D bottomRatRB;

    [Header("Movement Settings")]
    public float baseSpeed = 5f;
    public float jumpForce = 3f;
    public int score = 0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip landClip;
    [Range(0f, 1f)] public float audioVolume = 1f;

    private float currentSpeed;
    private float walkTimer = 0f;
    private float walkCooldown = 0.3f;
    private bool wasGroundedTop = true;
    private bool wasGroundedBottom = true;

    [Header("Ground Detection")]
    public Transform topGroundCheck;
    public Transform bottomGroundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Speed Acceleration")]
    public float speedIncreaseRate = 0.1f; // units per second


    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
            audioSource.volume = audioVolume;
    }

    void Update()
    {
        // Update volume live (in case user tweaks in Inspector during play)
        if (audioSource != null)
            audioSource.volume = audioVolume;

        // Move the whole player object forward
        baseSpeed += speedIncreaseRate * Time.deltaTime; // gradual acceleration
        currentSpeed = baseSpeed + (score * 3); // still supports score scaling

        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

        // Handle jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded(topGroundCheck))
                topRatRB.linearVelocity = new Vector2(topRatRB.linearVelocity.x, jumpForce);

            if (IsGrounded(bottomGroundCheck))
                bottomRatRB.linearVelocity = new Vector2(bottomRatRB.linearVelocity.x, jumpForce);
        }

        // Ground checks
        bool groundedTop = IsGrounded(topGroundCheck);
        bool groundedBottom = IsGrounded(bottomGroundCheck);

        // Play land sound
        if ((!wasGroundedTop && groundedTop) || (!wasGroundedBottom && groundedBottom))
        {
            audioSource.PlayOneShot(landClip);
        }

        // Play walk sound
        if (groundedTop || groundedBottom)
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0f)
            {
                audioSource.PlayOneShot(walkClip);
                walkTimer = walkCooldown;
            }
        }

        wasGroundedTop = groundedTop;
        wasGroundedBottom = groundedBottom;
    }

    bool IsGrounded(Transform groundCheck)
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void IncreaseScore()
    {
        score++;
    }

    void OnDrawGizmosSelected()
    {
        if (topGroundCheck != null)
            Gizmos.DrawWireSphere(topGroundCheck.position, groundCheckRadius);
        if (bottomGroundCheck != null)
            Gizmos.DrawWireSphere(bottomGroundCheck.position, groundCheckRadius);
    }
    void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
    {
        Debug.Log("Hit Obstacle!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver(); 
        }
    }
}


}
