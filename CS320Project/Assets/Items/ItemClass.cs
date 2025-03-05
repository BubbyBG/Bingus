using UnityEngine;

public class ItemClass : MonoBehaviour //This is the parent of weapons and usables
{
    //Every item has these properties
    public Vector3 basePosition; //Base location on screen from first person POV
    public Vector3 baseAngle;
    public Sprite guiSprite;
    public bool held; //Not in an inventory
    public string itemName;
    private Rigidbody rbody;
    private BoxCollider mcollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        held = false; //this might be obsolete with the new OnEjection method
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEjection(Vector3 ejectionVector)
    {
        mcollider = gameObject.AddComponent<BoxCollider>();
        mcollider.size = new Vector3(0.5f, 0.5f, 0.5f);
        mcollider.excludeLayers = LayerMask.GetMask("Entities");
        rbody = gameObject.AddComponent<Rigidbody>();
        rbody.AddForce(ejectionVector, ForceMode.VelocityChange);
    }
}
