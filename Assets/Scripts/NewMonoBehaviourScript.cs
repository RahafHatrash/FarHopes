using UnityEngine;

public class ColliderFlipper : MonoBehaviour
{

    [SerializeField] private Transform rat1; // Bottom collider
    [SerializeField] private Transform rat2; // Top collider

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            SwapPositions();
        }
    }

    void SwapPositions()
    {
        Vector3 tempPos = rat1.position;
        rat1.position = rat2.position;
        rat2.position = tempPos;

        Debug.Log("Swapped: Rat1 is now at " + rat1.position + " | Rat2 is now at " + rat2.position);
    }
}