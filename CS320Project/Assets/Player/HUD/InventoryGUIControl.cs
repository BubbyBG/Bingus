using UnityEngine;

public class InventoryGUIControl : MonoBehaviour
{
    //The two panel containers are this object's children
    private GameObject _quickContainer;
    private GameObject _mainContainer;
    public GameObject _mouseObject;
    private GameObject mouseObject;
    public GameObject playerObject;
    public Inventory inventory;
    private PlayerInput playerInput;

    void Start()
    {
        Cursor.visible = false; //hide cursor by default
        _quickContainer = transform.GetChild(0).gameObject;
        _mainContainer = transform.GetChild(1).gameObject;
        _mainContainer.SetActive(false); //hide inventory by default
        inventory = playerObject.GetComponent<Inventory>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        mouseObject = null;
        RefreshAll();
    }

    void Update()
    {
        //RefreshAll();
    }

    public void OpenInventory()
    {
        _mainContainer.SetActive(true);
        mouseObject = Instantiate(_mouseObject, transform);
        mouseObject.GetComponent<GUIMouse>().playerArms = playerObject.transform.GetChild(0).GetChild(0).GetComponent<PlayerArms>();
        Cursor.visible = true;
        RefreshAll();
    }
    
    public void CloseInventory()
    {
        _mainContainer.SetActive(false);
        Destroy(mouseObject);
        Cursor.visible = false;
        RefreshAll();
    }

    public void RefreshAll()
    {
        //Hotkey bar
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<InventoryPanel>().Refresh();
        }
        //Main grid
        if (playerInput.inventoryIsOpen)
        {
            for (int i = 0; i < transform.GetChild(1).childCount; i++)
            {
                transform.GetChild(1).GetChild(i).GetComponent<InventoryPanel>().Refresh();
            }
        }
    }
}
