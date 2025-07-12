using UnityEngine;

public class SizeSwapper : MonoBehaviour
{
    public Transform mouseA;
    public Transform mouseB;

    public Vector3 defaultSize = new Vector3(1f, 1f, 1f);
    public Vector3 bigSize = new Vector3(1.5f, 1.5f, 1f);
    public Vector3 smallSize = new Vector3(0.8f, 0.8f, 1f);

    public float smoothSpeed = 5f;

    private bool hasSwapped = false;
    private bool isASmall = true;

    private Vector3 targetScaleA;
    private Vector3 targetScaleB;

    void Start()
    {
        // البداية نفس الحجم
        mouseA.localScale = defaultSize;
        mouseB.localScale = defaultSize;

        targetScaleA = defaultSize;
        targetScaleB = defaultSize;
    }

    void Update()
    {
        // تحريك الحجم تدريجياً
        mouseA.localScale = Vector3.Lerp(mouseA.localScale, targetScaleA, Time.deltaTime * smoothSpeed);
        mouseB.localScale = Vector3.Lerp(mouseB.localScale, targetScaleB, Time.deltaTime * smoothSpeed);

        // التبديل عند الضغط
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !hasSwapped)
        {
            isASmall = true;
            SetTargets();
            hasSwapped = true;
        }
        else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && hasSwapped)
        {
            isASmall = !isASmall;
            SetTargets();
        }
    }

    void SetTargets()
    {
        if (isASmall)
        {
            targetScaleA = smallSize;
            targetScaleB = bigSize;
        }
        else
        {
            targetScaleA = bigSize;
            targetScaleB = smallSize;
        }
    }
}
