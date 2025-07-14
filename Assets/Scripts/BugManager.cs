using UnityEngine;

public class BugManager : MonoBehaviour
{
    public GameObject antPrefab;
    public int antCount = 5;
    public float spawnRadius = 2f;
    public float spawnDelay = 0.2f;

    void Start()
    {
        StartCoroutine(SpawnAnts());
    }

    System.Collections.IEnumerator SpawnAnts()
    {
        for (int i = 0; i < antCount; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            GameObject ant = Instantiate(antPrefab, spawnPos, Quaternion.identity);

            // Optionally face ants in random direction
            float flipX = Random.value > 0.5f ? 1 : -1;
            ant.transform.localScale = new Vector3(flipX, 1, 1);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
