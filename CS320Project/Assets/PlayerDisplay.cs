using UnityEditor;
using UnityEngine;

/* First person display class
controls the model of the currently held item
and at some point in the future, the HUD
*/

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject blankObjectDefinition;
    private GameObject heldItemType; //the item currently held, or null for none
    private GameObject heldItem;
    private Inventory inventory; //ref to object where the inventory is
    private int activeSlot; //slot in inventory currently active (held)

    void Start()
    {
        inventory = transform.parent.parent.GetComponent<Inventory>();
        activeSlot = 0;
        heldItemType = inventory.GetItem(activeSlot);
        EquipItem();
    }

    void Update()
    {
        //Item switching
        int lastInput = GetInput();
        if (lastInput != -1) //if a button was pressed
        {
            if (lastInput != activeSlot) //update if a new slot was selected
            {
                activeSlot = lastInput;
                //Check for null ???
                heldItemType = inventory.GetItem(activeSlot);
                EquipItem();
            }
        }

        UpdateItemLocation();
    }

    public void EquipItem() //update item model etc
    {
        if (heldItemType == null)
        {
            Destroy(heldItem);
            heldItem = Instantiate(blankObjectDefinition, gameObject.transform, false);
        } 
        else
        {
            Destroy(heldItem);
            heldItem = Instantiate(heldItemType, gameObject.transform, false);
            ItemClass heldItemClass = heldItem.GetComponent<ItemClass>();
            heldItem.transform.localPosition = heldItemClass.basePosition;
            heldItem.transform.localEulerAngles = heldItemClass.baseAngle;
        }
    }

    public void UpdateItemLocation()
    {
            ItemClass heldItemClass = heldItem.GetComponent<ItemClass>();
            heldItem.transform.localPosition = heldItemClass.basePosition;
            heldItem.transform.localEulerAngles = heldItemClass.baseAngle;
    }

    public int GetInput() //there's probably a better way to do this
    {
        //Alpha# means alphanumeric keyboard key #
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 4;
        }

        return -1;
    }
}
