using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// collects all predefined spawn points of a section of the map
// randomly chooses a set of the points to spawn items on
public class ItemSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints; // stores positions of spawnpoints
    public List<Transform> selectedPoints;
    public int maxItems;
    // set positions of spawnable items
    void Start()
    {
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        SelectPoints();
    }
    public void SelectPoints()
    {
        while (selectedPoints.Count <= maxItems)
        {
            int randIndex = Random.Range(0, spawnPoints.Count);
            selectedPoints.Add(spawnPoints[randIndex]);
            spawnPoints.RemoveAt(randIndex);
        }
    }
}