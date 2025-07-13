using UnityEngine;

public class RatCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ScoreManager.Instance.GameOver();
        }
    }
}
