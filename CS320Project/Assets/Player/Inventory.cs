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
            //inventorySlot[i] = null;
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

    public bool AddItem(GameObject itemToAdd) //when the player picks something up ingame; chooses next available spot
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventorySlot[i] == null)
            {
                inventorySlot[i] = itemToAdd;
                return true;
            }
        }
        //else if inventory is full
        print("Inventory is full");
        return false;
    }

    public void InsertItem(GameObject itemToAdd, int position) //inserts at a position
    {
        if (inventorySlot[position] == null)
        {
            inventorySlot[position] = itemToAdd;
            ItemClass item = inventorySlot[position].GetComponent<ItemClass>();
            item.inWorldSpace = false;
            return;
        }
    }

    public bool DropItem(int slotNumber) //when the player drops something up ingame
    {
        if (inventorySlot[slotNumber] != null)
        {
            GameObject droppedItem = Instantiate(inventorySlot[slotNumber], transform.position, transform.rotation);
            Vector3 ejectionVector = transform.forward * 2 + transform.up * 2;
            droppedItem.GetComponent<ItemClass>().OnEjection(ejectionVector);
            inventorySlot[slotNumber] = null;
            return true;
        }
        return false;
    }

    public void RemoveItem(int slotNumber) //when the player drops something up ingame
    {
        inventorySlot[slotNumber] = null;
    }

}