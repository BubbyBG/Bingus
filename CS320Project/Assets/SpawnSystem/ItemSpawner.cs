using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour //attach ItemSpawner to spawnzoneprefab
{
    public List<Transform> spawnPoints; // stores positions of spawnpoints
    public List<Transform> selectedPoints;
    private int itemQuantity;

    public void SelectPoints()
    {
        while ((selectedPoints.Count <= itemQuantity) && (spawnPoints.Count > 0))
        {
            int randIndex = Random.Range(0, spawnPoints.Count);
            selectedPoints.Add(spawnPoints[randIndex]);
            spawnPoints.RemoveAt(randIndex);
        }
    }

    public void SpawnItems(Dictionary<GameObject, int> items) // spawn multiple item types over spawn points
    {
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());

        foreach (var item in items)
        {
            itemQuantity = item.Value;
            SelectPoints();

            for (int i = 0; i < itemQuantity; i++)
            {
                Transform itemSpawnPoint = selectedPoints[i];
                Instantiate(item.Key, itemSpawnPoint.position, itemSpawnPoint.rotation);
            }
            selectedPoints.Clear();
        }
    }
}