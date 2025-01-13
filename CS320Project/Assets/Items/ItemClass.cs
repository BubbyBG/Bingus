using UnityEngine;

public class ItemClass : MonoBehaviour //This is the parent of weapons and usables
{
    //Every item has these properties

    //Base location on screen from first person POV
    float offsetX; 
    float offsetY;
    //Additional offset for walking sway
    float swayX; 
    float swayY;
    //Additional offset for looking sway
    float overlayX;
    float overlayY;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
