using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public float baseSpeed = 5f;
    public int score = 0;

    public float jumpForce = 7f;
    private Rigidbody2D rb;

    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentSpeed = baseSpeed + (score * 3);
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
    
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
        }
    }

    public void IncreaseScore()
    {
        score++;
    }
   void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
    {
       GameManager.Instance.GameOver();

    }
}


}
