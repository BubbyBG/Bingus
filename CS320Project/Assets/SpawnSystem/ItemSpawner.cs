using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour //attach ItemSpawner to spawnzoneprefab
{
    private List<Transform> spawnPoints; // stores positions of spawnpoints
    private List<Transform> selectedPoints;
    private int itemQuantity;

    public void SelectPoints()
    {
        while ((selectedPoints.Count < itemQuantity) && (spawnPoints.Count > 0))
        {
            int randIndex = Random.Range(0, spawnPoints.Count);
            selectedPoints.Add(spawnPoints[randIndex]);
            spawnPoints.RemoveAt(randIndex);
        }
    }

    public void SpawnItems(Dictionary<GameObject, int> items) // spawn multiple item types over spawn points
    {
        // spawnPoints.Clear();
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());

        foreach (var item in items)
        {
            itemQuantity = item.Value;
            SelectPoints();

            for (int i = 0; i < itemQuantity; i++)
            {
                if (i >= selectedPoints.Count)
                {
                    Debug.Log("spawn points error");
                    break;
                }
                Transform itemSpawnPoint = selectedPoints[i];
                Instantiate(item.Key, itemSpawnPoint.position, itemSpawnPoint.rotation);
            }
            selectedPoints.Clear();
        }

    }
}