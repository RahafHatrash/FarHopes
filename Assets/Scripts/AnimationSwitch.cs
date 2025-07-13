using UnityEngine;

public class AnimationSwitch : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            animator.SetTrigger("RightClick");
        }
    }
}
