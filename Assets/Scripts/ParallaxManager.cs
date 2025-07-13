using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform firstObject;

    private Transform currentObject;
    private GameObject lastPrefabSpawned;

    private void Start()
    {
        lastPrefabSpawned = firstObject.gameObject;
        currentObject = firstObject.transform;
    }

    private void Update()
    {
        if (Camera.main.transform.position.x > currentObject.transform.position.x)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // var getRandomPrefab = prefabs[Random.Range(0, prefabs.Count)];
        var getRandomPrefab = GetNewRandomPrefab();
        var spriteWidth = prefabs[0].GetComponent<SpriteRenderer>().bounds.extents.x;
        var newPosition = new Vector3(currentObject.transform.position.x + spriteWidth * 2, transform.position.y, transform.position.z);
        var newObject = Instantiate(getRandomPrefab, newPosition, Quaternion.identity);
        currentObject = newObject.transform;
    }

    private GameObject GetNewRandomPrefab()
    {
        GameObject newPrefab = GetRandomPrefab();
        while (newPrefab.name == lastPrefabSpawned.name && prefabs.Count > 1)
        {
            newPrefab = GetRandomPrefab();
        }
        lastPrefabSpawned = newPrefab;
        return newPrefab;
    }

    private GameObject GetRandomPrefab()
    {
        return prefabs[Random.Range(0, prefabs.Count)];
    }
}
