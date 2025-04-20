using UnityEngine;
using System.Collections.Generic;

public class SpawnSystem : MonoBehaviour// manages all tokenItem spawn groupings in map
{
    public GameObject firstAidKit; // spawnable items
    public GameObject book;
    public ItemSpawner mapSection1; // spawn location groupings
    public ItemSpawner mapSection2;

    void Start()
    {
        Dictionary<GameObject, int> mapSection1Items = new Dictionary<GameObject, int>
        {
            { firstAidKit, 10}
            ,{ book, 10}
        };
        mapSection1.SpawnItems(mapSection1Items);
        Dictionary<GameObject, int> mapSection2Items = new Dictionary<GameObject, int>
        {
            { firstAidKit, 5}
            ,{ book, 5}
        };
        mapSection2.SpawnItems(mapSection2Items);
    }

}