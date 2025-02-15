using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private int slotNumber;
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
    }

    void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (inventory.GetItem(slotNumber) != null)
        {
            item = inventory.GetItem(slotNumber).GetComponent<ItemClass>();
            sprite.sprite = item.guiSprite;
        }
        else
        {
            sprite.sprite = defaultImage;
        }

        if (input.activeSlot == slotNumber)
        {
            sprite.color = Color.white;
        }
        else
        {
            sprite.color = defaultColor;
        }
    }

    public int GetNumber()
    {
        if (inventory.GetItem(slotNumber) != null)
        {
            print(item.itemName);
            return slotNumber;
        }
        else
        {
            print("ERROR");
            return -1;
        }
    }
}
