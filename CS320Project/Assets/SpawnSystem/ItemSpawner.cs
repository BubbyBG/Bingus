using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour //attach ItemSpawner to spawnzoneprefab
{
    public BoxCollider spawnZone;
    public List<Vector3> selectedPoints;
    private int itemQuantity;

    void Start()
    {
        spawnZone = GetComponent<BoxCollider>();
    }

    public void SelectPoints()
    {
        while ((selectedPoints.Count <= itemQuantity))
        {
            Vector3 randIndex = new Vector3(
                Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
                Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y),
                Random.Range(spawnZone.bounds.min.z, spawnZone.bounds.max.z)
            );
            selectedPoints.Add(randIndex);
        }
    }

    public void SpawnItems(Dictionary<GameObject, int> items) // spawn multiple item types over spawn points
    {
        foreach (var item in items)
        {
            itemQuantity = item.Value;
            SelectPoints();

            for (int i = 0; i < itemQuantity; i++)
            {
                Vector3 itemSpawnPoint = selectedPoints[i];
                Instantiate(item.Key, itemSpawnPoint, Quaternion.identity);
            }
            selectedPoints.Clear();
        }
    }
}