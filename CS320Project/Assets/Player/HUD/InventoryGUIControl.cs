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

    void Start()
    {
        Cursor.visible = false; //hide cursor by default
        _quickContainer = transform.GetChild(0).gameObject;
        _mainContainer = transform.GetChild(1).gameObject;
        _mainContainer.SetActive(false); //hide inventory by default
        inventory = playerObject.GetComponent<Inventory>();
        mouseObject = null;
    }

    public void OpenInventory()
    {
        _mainContainer.SetActive(true);
        mouseObject = Instantiate(_mouseObject, transform);
        Cursor.visible = true;
    }
    
    public void CloseInventory()
    {
        _mainContainer.SetActive(false);
        Destroy(mouseObject);
        Cursor.visible = false;
    }
}
