using UnityEngine;
using UnityEngine.UI;

public class HotkeyPanel : MonoBehaviour
{
    [SerializeField]
    private int slotNumber;
    [SerializeField]
    private GameObject inventoryObject;
    private Inventory inventory;
    private Image sprite;
    [SerializeField]
    private Sprite defaultImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory>();
        sprite = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (inventory.GetItem(slotNumber) != null)
        {
            ItemClass item = inventory.GetItem(slotNumber).GetComponent<ItemClass>();
            sprite.sprite = item.guiSprite;
        }
        else
        {
            sprite.sprite = defaultImage;
        }
    }
}
