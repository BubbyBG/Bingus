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
    private Vector3 swayVector;
    public bool updateArms;

    [SerializeField]
    private float maxSwayStanding = 0.11f;
    [SerializeField]
    private float maxSwayWalking = 0.11f;
    [SerializeField]
    private float maxSwayRotation = 0.11f;
    [SerializeField]
    private float swayFrequencyStanding = 0.11f;

    void Start()
    {
        inventory = transform.parent.parent.GetComponent<Inventory>();
        input = transform.parent.parent.GetComponent<PlayerInput>();
        heldItemType = inventory.GetItem(input.activeSlot);
        swayVector = new Vector3(0f, 0f, 0f);
        updateArms = true;
        EquipItem();
    }

    void Update()
    {
        UpdateItemLocation();
    }

    public void EquipItem() //update item model etc
    {
        if (heldItemType == null)
        {
            Destroy(heldItem);
            heldItem = Instantiate(blankObjectDefinition, transform, false);
        } 
        else
        {
            Destroy(heldItem);
            heldItem = Instantiate(heldItemType, transform, false);
            ItemClass heldItemClass = heldItem.GetComponent<ItemClass>();
            heldItem.transform.localPosition = heldItemClass.basePosition;
            heldItem.transform.localEulerAngles = heldItemClass.baseAngle;
        }
    }

    public void UpdateItemLocation()
    {
        swayVector.y = maxSwayStanding * Mathf.Sin(swayFrequencyStanding * Time.time);
        ItemClass heldItemClass = heldItem.GetComponent<ItemClass>();
        heldItem.transform.localPosition = heldItemClass.basePosition + swayVector;
        heldItem.transform.localEulerAngles = heldItemClass.baseAngle;
    }

    public void SwitchSlot(int lastInput)
    {
<<<<<<< Updated upstream:CS320Project/Assets/Player/PlayerDisplay.cs
        if (lastInput != input.activeSlot) //update if a new slot was selected
        {
            input.activeSlot = lastInput;
            //Check for null ???
            heldItemType = inventory.GetItem(input.activeSlot);
            EquipItem();
        }
=======
        input.activeSlot = inputNum;
        heldItemType = inventory.GetItem(input.activeSlot);
        EquipItem();
    }

    public void UpdateArms()
    {
        SwitchSlot(input.activeSlot);
>>>>>>> Stashed changes:CS320Project/Assets/Player/PlayerArms.cs
    }
}
