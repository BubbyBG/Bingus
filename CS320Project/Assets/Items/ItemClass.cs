using UnityEngine;

public class ItemClass : MonoBehaviour //This is the parent of weapons and usables
{
    //Every item has these properties
    public Vector3 basePosition; //Base location on screen from first person POV
    public Vector3 baseAngle;
    public Sprite guiSprite;
    public bool held; //Not in an inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        held = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
