using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// collects all predefined spawn points of a section of the map
// randomly chooses a set of the points to spawn items on
public class ItemSpawner : MonoBehaviour //attach ItemSpawner to spawnzoneprefab
{
    public List<Transform> spawnPoints; // stores positions of spawnpoints
    public List<Transform> selectedPoints;
    public int itemQuantity;

    public void SelectPoints()
    {
        while (selectedPoints.Count < itemQuantity)
        {
            int randIndex = Random.Range(0, spawnPoints.Count);
            selectedPoints.Add(spawnPoints[randIndex]);
            spawnPoints.RemoveAt(randIndex);
        }
    }

    public void SpawnItems(GameObject itemObjectName, int itemQuantity) // spawn 1 item type
    {
        this.itemQuantity = itemQuantity;
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        SelectPoints();
        for (int i = 0; i < itemQuantity; i++)
        {
            Transform itemSpawnPoint = selectedPoints[i];
            Instantiate(itemObjectName, itemSpawnPoint);
        }
    }
}