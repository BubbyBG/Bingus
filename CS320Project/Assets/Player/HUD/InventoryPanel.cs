using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    public int slotNumber;
    [SerializeField]
    private GameObject player;
    private Inventory inventory;
    private PlayerInput input;
    private Image sprite;
    [SerializeField]
    private Sprite defaultImage;
    public ItemClass item;
    private Graphic graphic;
    private Color defaultColor;
    public enum Type
    {
        quickSlot,
        mainInventory
    }
    public Type types;

    void Start()
    {
        inventory = player.GetComponent<Inventory>();
        input = player.GetComponent<PlayerInput>();
        graphic = GetComponent<Graphic>();
        defaultColor = graphic.color;
        sprite = transform.GetChild(0).GetComponent<Image>();

        if (inventory.GetItem(slotNumber) != null)
        {
            item = inventory.GetItem(slotNumber).GetComponent<ItemClass>();
        }
        else
        {
            item = null;
        }
        Refresh();
    }

    void Awake()
    {
        Start();
    }

    public void Refresh()
    {
        inventory = player.GetComponent<Inventory>();
        if (inventory.GetItem(slotNumber) != null)
        {
            item = inventory.GetItem(slotNumber).GetComponent<ItemClass>();
            sprite.sprite = item.guiSprite;
        }
        else
        {
            sprite.sprite = defaultImage;
        }
    }

    public GameObject GetItem()
    {
        return inventory.GetItem(slotNumber);
    }

    public void RemoveItem()
    {
        inventory.RemoveItem(slotNumber);
    }

    public void AddItem(GameObject item, int position)
    {
        inventory.InsertItem(item, position);
    }
}
