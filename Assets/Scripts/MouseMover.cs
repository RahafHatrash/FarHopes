using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public float baseSpeed = 5f;
    public int score = 0;

    public float jumpForce = 7f;

    public Transform leftRat;
    public Transform rightRat;

    private Rigidbody2D rbLeft;
    private Rigidbody2D rbRight;

    private float currentSpeed;

    private Animator animator;

    void Start()
    {
        rbLeft = leftRat.GetComponent<Rigidbody2D>();
        rbRight = rightRat.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Move entire system forward
        currentSpeed = baseSpeed + (score * 3);
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(1)) // 1 = Right Click
        {
            animator.SetTrigger("RightClick");
        }

        // Jump both rats
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded(rbLeft))
                rbLeft.linearVelocity = new Vector2(rbLeft.linearVelocity.x, jumpForce);

            if (IsGrounded(rbRight))
                rbRight.linearVelocity = new Vector2(rbRight.linearVelocity.x, jumpForce);
        }
    }

    bool IsGrounded(Rigidbody2D rb)
    {
        return Mathf.Abs(rb.linearVelocity.y) < 0.01f;
    }

    public void IncreaseScore()
    {
        score++;
    }
}