using UnityEngine;

public class BugManager : MonoBehaviour
{
    public GameObject bugPrefab;
    public int maxBugs = 2; // عدد الحشرات المطلوب فقط
    public float spawnRadius = 4f;

    private int spawnedBugs = 0;

    void Start()
    {
        for (int i = 0; i < maxBugs; i++)
        {
            SpawnBug();
        }
    }

    void SpawnBug()
    {
        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject bug = Instantiate(bugPrefab, spawnPos, Quaternion.identity);

        spawnedBugs++;
    }
}
