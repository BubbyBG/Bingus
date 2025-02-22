using Unity.VisualScripting;
using UnityEngine;

public class ItemClass : MonoBehaviour //This is the parent of weapons and usables
{
    //Every item has these properties
    public Vector3 basePosition; //Base location on screen from first person POV
    public Vector3 baseAngle;
    public Sprite guiSprite;
<<<<<<< Updated upstream
    public bool held; //Not in an inventory
=======
    public string itemName;
    public bool held = false; //not held when placed on map outside of player's inventory
    private bool physicsOn = false;
    MeshCollider meshCollider;
    Rigidbody rigidBody;
>>>>>>> Stashed changes

    void Start()
    {
        meshCollider = transform.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        rigidBody = transform.AddComponent<Rigidbody>();
        if (transform.childCount > 0)
        {
            meshCollider.sharedMesh = transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        }
    }

    void Update()
    {
        if (held && physicsOn)
        {
            DisablePhysics();
        }
        else if (!held && !physicsOn)
        {
            EnablePhysics();
        }
    }

    public void EnablePhysics()
    {
        if (transform.childCount > 0)
        {
            meshCollider.sharedMesh = transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        }
        meshCollider.enabled = true;
        rigidBody.isKinematic = false;
        physicsOn = true;
    }

    public void DisablePhysics()
    {
        meshCollider.enabled = false;
        rigidBody.isKinematic = true;
        physicsOn = false;
    }
}
