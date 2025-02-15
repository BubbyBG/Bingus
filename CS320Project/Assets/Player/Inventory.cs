using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int inventorySize = 20;
    [SerializeField] //This is the grid containing items:
    private GameObject[] inventorySlot = new GameObject[inventorySize];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventorySlot[i] != null)
            {
                ItemClass item = inventorySlot[i].GetComponent<ItemClass>();
                item.held = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetItem(int slotNumber)
    {
        return inventorySlot[slotNumber];
    }

    public void AddItem(GameObject itemToAdd) //when the player picks something up ingame
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventorySlot[i] == null)
            {
                inventorySlot[i] = itemToAdd;
                ItemClass item = inventorySlot[i].GetComponent<ItemClass>();
                item.held = true;
                return;
            }
        }
        print("Inventory is full");
    }

    public void DropItem(int slotNumber) //when the player drops something up ingame
    {
        if (inventorySlot[slotNumber] != null)
        {
            GameObject _droppedItem = Instantiate(inventorySlot[slotNumber], transform.position, transform.rotation);
            ItemClass droppedItem = _droppedItem.GetComponent<ItemClass>();
            droppedItem.held = false;
            inventorySlot[slotNumber] = null;
        }
    }

    public GameObject RemoveItem(int slotNumber) //when the player drops something up ingame
    {
        if (inventorySlot[slotNumber] != null)
        {
            GameObject _removedItem = inventorySlot[slotNumber];
            inventorySlot[slotNumber] = null;
            return _removedItem;
        }
        else
        {
            return null;
        }
    }

}
