using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    /*
    This script was made for a central controller for gui, inventory and held item events

    */
    //references
    public GameObject _playerArms;
    private PlayerArms playerArms;
    private Inventory inventory;
    public int activeSlot = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerArms = _playerArms.GetComponent<PlayerArms>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
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
<<<<<<< Updated upstream
        activeSlot = num;
        playerArms.SwitchSlot(num);
=======
        if (num != activeSlot)
        {
            playerArms.SwitchSlot(num);
            activeSlot = num;
        }
>>>>>>> Stashed changes
        return;
    }

    public void OnInventoryOpenKey()
    {
        return;
    }

    public void OnItemDropKey()
    {
        inventory.DropItem(activeSlot);
<<<<<<< Updated upstream
        playerArms.SwitchSlot(activeSlot);
        return;
=======
        playerArms.EquipItem();
        masterHUD.RefreshAll();
>>>>>>> Stashed changes
    }

    public void OnUseKey()
    {
        return;
    }
}
