using UnityEngine;
using System.Collections;

public class Drawer : MonoBehaviour, IInteractable
{
    public Transform pivot;
    public float openSpeed = 50f;
    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Vector3 currentPosition;

    void Awake()
    {
        if (pivot == null) pivot = transform;
    }

    void Start()
    {
        closedPosition = pivot.position;
        Renderer rend = GetComponent<Renderer>();
        float width = rend.bounds.size.z;

        openPosition = closedPosition - pivot.forward * width;

        currentPosition = closedPosition; // start closed
    }

    public void Interact()
    {
        Debug.Log("Drawer interacted!");
        
        isOpen = !isOpen;
        currentPosition = isOpen ? openPosition : closedPosition;
        
    }

    void Update()
    {
        if (Vector3.Distance(pivot.position, currentPosition) > 0.01f)
        {
            pivot.position = Vector3.MoveTowards(
                pivot.position,
                currentPosition,
                openSpeed * Time.deltaTime
            );
        }
    }

}
