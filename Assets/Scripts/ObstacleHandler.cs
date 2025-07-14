using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private AudioClip hitSound; // ✅ Add your sound here

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            // 💥 Spawn hit effect
            if (deathParticlePrefab != null)
            {
                GameObject particle = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
                Destroy(particle, 2f); // cleanup
            }

            // 🔊 Play hit sound
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, 1.5f); // ✅ volume = 1.5f
            }

            // 🎬 Trigger death animation and game over
            playerAnimator.SetTrigger("Die");
            GameManager.Instance.GameOver();
        }
    }
}
