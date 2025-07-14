using UnityEngine;

public class AnimationSwitch : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private AudioClip switchSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        animator.SetBool("isBig", true);
        animator.SetBool("isBig", false);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("RightClick");
            audioSource.PlayOneShot(switchSound, 1f); // louder & cleaner
        }
    }
}