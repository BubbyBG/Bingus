using UnityEngine;

public class ItemClass : MonoBehaviour
{
    
    
    //Every item has these properties
    public Vector3 basePosition; //Base location on screen from first person POV
    public Vector3 baseAngle;
    public Sprite guiSprite;
    public bool inWorldSpace;
    public bool held;
    public string itemName;
    private Rigidbody rbody;
    private BoxCollider mcollider;
    public bool placed;
    public enum itemType
    {
        noItem,
        gunItem,
        meleeItem,
        musicBox,
        uselessItem,
    }
    public itemType type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SceneStart() //only for objects placed in the scene
    {
        print("Called scene start");
        if (type != itemType.noItem)
        {
            Vector3 newPos = new Vector3(0,0,0);
            OnEjection(newPos); //Start with rigidbody
        }
    }

    public void StartOnEquip() //
    {
        
        held = true;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        inWorldSpace = true;
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
        //rbody = gameObject.AddComponent<Rigidbody>();
        //rbody.AddForce(ejectionVector, ForceMode.VelocityChange);
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        inWorldSpace = true;
        
    }

    public GameObject OnUseKey() //when the player looks at it and presses the use button
    {
        //Destroy(rbody);
        //Destroy(mcollider);
        //transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        //inWorldSpace = false;
        return gameObject;
    }
}
