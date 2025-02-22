using UnityEngine;
using UnityEngine.UI;

public class GUIMouse : MonoBehaviour
{
    public GameObject grabbedItem;
    [SerializeField]
    private GameObject iconDisplayerDef;
    private GameObject iconDisplayer;
    public GameObject gotItem;
    private InventoryGUIControl control;
    public GameObject player;
    private PlayerArms playerArms;
    //private PlayerInput playerInput;

    void Start()
    {
        iconDisplayer = null;
        grabbedItem = null;
        control = transform.parent.GetComponent<InventoryGUIControl>();
        player = control.playerObject;
        playerArms = player.transform.GetChild(0).GetChild(0).GetComponent<PlayerArms>();
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
                gotItem = panel.GetItem();
                if (grabbedItem == null && gotItem != null) //empty hand, full slot; pick up item
                {
                    grabbedItem = gotItem;
                    panel.RemoveItem();
                    PutOnCursor(grabbedItem);
                    panel.Refresh();
                }
                else if (grabbedItem != null && gotItem == null) //full hand, empty slot; place item
                {
                    panel.AddItem(grabbedItem, panel.slotNumber);
                    grabbedItem = null;
                    EmptyCursor();
                    panel.Refresh();
                }
                else if (grabbedItem != null && gotItem != null) //full hand, full slot; swap items
                {
                    GameObject previouslyHeld = grabbedItem;
                    grabbedItem = gotItem;
                    panel.RemoveItem();
                    panel.AddItem(previouslyHeld, panel.slotNumber);
                    EmptyCursor();
                    PutOnCursor(gotItem);
                    control.RefreshAll();
                }
                playerArms.UpdateArms();
            }
        }
    }

    private void PutOnCursor(GameObject obj)
    {
        //Retrieve correct sprite
        Sprite sprite = obj.GetComponent<ItemClass>().guiSprite;
        //Attach sprite to mouse
        iconDisplayer = Instantiate(iconDisplayerDef, transform);
        iconDisplayer.GetComponent<Image>().sprite = sprite;
    }

    private void EmptyCursor()
    {
        Destroy(iconDisplayer);
    }
}
