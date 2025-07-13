using UnityEngine;

public class BugMover : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector2 direction;

    void Start()
    {
        direction = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
