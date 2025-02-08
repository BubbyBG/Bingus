using UnityEngine;

public class Map : MonoBehaviour
{
    public Terrain terrain;
    public GameObject player;
    public GameObject[] objects;
    
    void Start()
    {
        Debug.Log("Map started!");
        initializeMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeMap(){
        //set up fog at edge
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.5f, 0.6f, 0.7f, 1); // Light blue
        RenderSettings.fogDensity = 0.01f;
        RenderSettings.fogMode = FogMode.Linear;
    }
}
