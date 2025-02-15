using UnityEditor;
using UnityEngine;

/* First person display class
controls the model of the currently held item
and at some point in the future, the HUD
*/

public class PlayerArms : MonoBehaviour
{
    [SerializeField]
    private GameObject blankObjectDefinition;
    private GameObject heldItemType; //the item currently held, or null for none
    private GameObject heldItem;
    private Inventory inventory; //ref to object where the inventory is
    private PlayerInput input;

    void Start()
    {
        inventory = transform.parent.parent.GetComponent<Inventory>();
        input = transform.parent.parent.GetComponent<PlayerInput>();
        heldItemType = inventory.GetItem(input.activeSlot);
        EquipItem();
    }

    void Update()
    {
        UpdateItemLocation();
    }

    public void EquipItem() //update item model etc
    {
        heldItemType = inventory.GetItem(input.activeSlot);
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

    public void SwitchSlot(int inputNum)
    {
        if (inputNum != input.activeSlot) //update if a new slot was selected
        {
            input.activeSlot = inputNum;
            //Check for null ???
            heldItemType = inventory.GetItem(input.activeSlot);
            EquipItem();
        }
    }
}
