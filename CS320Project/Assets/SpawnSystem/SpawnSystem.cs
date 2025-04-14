using UnityEngine;
using System.Collections.Generic;

public class SpawnSystem : MonoBehaviour// manages all tokenItem spawn groupings in map
{
    public GameObject firstAidKit; // spawnable items
    // public GameObject book;
    public ItemSpawner mapSection1; // spawn location groupings

    void Start()
    {
        Dictionary<GameObject, int> mapSection1Items = new Dictionary<GameObject, int>
        {
            { firstAidKit, 2}
            // ,{ book, 1}
        };
        mapSection1.SpawnItems(mapSection1Items);
    }
}