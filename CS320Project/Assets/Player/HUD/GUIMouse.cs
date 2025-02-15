using UnityEngine;
using UnityEngine.UI;

public class GUIMouse : MonoBehaviour
{
    private GameObject grabbedItem;
    [SerializeField]
    private GameObject iconDisplayerDef;
    private GameObject iconDisplayer;
    public Inventory inventory;

    void Start()
    {
        iconDisplayer = null;
        grabbedItem = null;
        inventory = transform.parent.GetComponent<InventoryGUIControl>().inventory;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            EventClick();
        }
    }

    private void EventClick()
    {
        Vector2 mousePosition = Input.mousePosition;
        Collider2D clickedOn;
        clickedOn = Physics2D.OverlapPoint(mousePosition);

        if (clickedOn != null) //if a 2d object was clicked on
        {
            GameObject _obj = clickedOn.gameObject;
            InventoryPanel panel = _obj.GetComponent<InventoryPanel>();
            if (panel != null) //if the 2d object was an inventory panel
            {
                int num = panel.GetNumber();
                if (grabbedItem == null && panel.item != null) //empty hand, full slot; pick up item
                {
                    grabbedItem = inventory.RemoveItem(num);
                    PutOnCursor(grabbedItem);
                }
                else if (grabbedItem != null && panel.item == null) //full hand, empty slot; place item
                {
                    inventory.AddItem(grabbedItem);
                    grabbedItem = null;
                    EmptyCursor();
                }
                else if (grabbedItem != null && panel.item != null) //full hand, full slot; swap items
                {
                    GameObject previouslyHeld = grabbedItem;
                    grabbedItem = inventory.RemoveItem(num);
                    PutOnCursor(grabbedItem);
                    inventory.AddItem(previouslyHeld);
                }
            }
        }
    }

    public void PutOnCursor(GameObject obj)
    {
        //Retrieve correct sprite
        Sprite sprite = obj.GetComponent<ItemClass>().guiSprite;
        //Attach sprite to mouse
        iconDisplayer = Instantiate(iconDisplayerDef, transform);
        iconDisplayer.GetComponent<Image>().sprite = sprite;
    }

    public void EmptyCursor()
    {
        grabbedItem = null;
        Destroy(iconDisplayer);
    }
}
