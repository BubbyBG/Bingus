using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// manages all spawn groupings in map
public class SpawnSystem : MonoBehaviour
{
    public GameObject firstAidKit; // spawnable items
    // public GameObject book;
    public ItemSpawner mapSection1; // spawn location groupings

    void Start()
    {
        Dictionary<GameObject, int> mapSection1Items = new Dictionary<GameObject, int>
        {
            { firstAidKit, 1}
            // ,{ book, 1}
        };
        mapSection1.SpawnItems(mapSection1Items);
    }
}