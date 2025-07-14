using UnityEngine;

public class PositionSwapper : MonoBehaviour
{
    [Header("Objects to Move")]
    public Transform objectA;
    public Transform objectB;

    [Header("Target Positions")]
    public Vector3 newPositionA;
    public Vector3 newPositionB;

    private bool hasSwapped = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!hasSwapped)
            {
                if (objectA != null) objectA.position = newPositionA;
                if (objectB != null) objectB.position = newPositionB;
                hasSwapped = true;
            }
            else
            {
                // Optionally reverse (swap back)
                if (objectA != null) objectA.position = newPositionB;
                if (objectB != null) objectB.position = newPositionA;
                hasSwapped = false;
            }
        }
    }
}
