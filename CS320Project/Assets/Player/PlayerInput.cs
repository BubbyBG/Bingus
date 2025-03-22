using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    /*
    This script was made for a central controller for gui, inventory and held item events

    */
    //references
    public GameObject _playerArms;
    private PlayerArms playerArms;
    public GameObject _masterHUD;
    private InventoryGUIControl masterHUD;
    private Inventory inventory;
    public bool inventoryIsOpen = false;
    public int activeSlot;
    public LayerMask cantUse;
    public Vector3 posFrom;
    public Vector3 dirTo;
    private Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeSlot = 0;
        playerArms = _playerArms.GetComponent<PlayerArms>();
        inventory = GetComponent<Inventory>();
        masterHUD = _masterHUD.GetComponent<InventoryGUIControl>();
        cam = transform.GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        posFrom = cam.transform.position + Vector3.down * 0.1f; //lowered so its easily visible
        dirTo = cam.transform.forward;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnItemDropKey();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryOpenKey();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUseKey();
        }

        //Alpha# means alphanumeric keyboard key #
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnNumberKey(0);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnNumberKey(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnNumberKey(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnNumberKey(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OnNumberKey(4);
        }
    }

    public void OnNumberKey(int num)
    {
        if (activeSlot != num) //don't switch to the slot that's already active
        {
            activeSlot = num;
            playerArms.SwitchSlot();
        }
    }

    public void OnInventoryOpenKey()
    {
        if (inventoryIsOpen)
        {
            masterHUD.CloseInventory();
            inventoryIsOpen = false;
        }
        else
        {
            masterHUD.OpenInventory();
            inventoryIsOpen = true;
        }
    }

    public void OnItemDropKey()
    {
        if (inventory.DropItem(activeSlot) == true)
        {
            playerArms.SwitchSlot();
            masterHUD.RefreshAll();
        }
    }

    public void OnUseKey()
    {
        Debug.DrawRay(posFrom, dirTo, Color.white, 10f);
        RaycastHit hit;
        if (Physics.Raycast(posFrom, dirTo, out hit, 5f))
        {
            print(hit.transform.gameObject.ToString());// ToString());
            ItemClass hitThing = hit.transform.GetComponent<ItemClass>();
            if (hitThing != null)
            {
                if (inventory.AddItem(hit.transform.gameObject)) //if inventory has an empty slot
                {
                    hitThing.OnUseKey(); //destroy physics components on item
                    Destroy(hitThing.gameObject);
                    masterHUD.RefreshAll();
                }
            }
        }
    }
}
