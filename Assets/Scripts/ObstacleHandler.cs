using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}        
