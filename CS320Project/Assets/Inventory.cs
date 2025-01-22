using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int inventorySize = 20;
    [SerializeField] //This is the grid containing items:
    private GameObject[] inventorySlot = new GameObject[inventorySize];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                return;
            }
        }
        print("Inventory is full");
        return;
    }
}
