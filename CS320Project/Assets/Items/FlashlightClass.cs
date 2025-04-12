using UnityEngine;

public class FlashlightClass : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Light flashLight;
    void Start()
    {
        flashLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            flashLight.enabled = !flashLight.enabled;
        }
        
    }
}
