using UnityEngine;
using UnityEditor;

// manages all spawn groupings in map
// att
public class SpawnSystem : MonoBehaviour
{
    public GameObject firstAidKit;
    public ItemSpawner firstAidMapSection1;

    void Start() // spawn items
    {
        firstAidMapSection1.SpawnItems(firstAidKit, 2);
    }
}