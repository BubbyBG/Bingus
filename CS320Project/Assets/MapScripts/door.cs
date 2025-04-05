using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public Transform pivot;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (pivot == null) pivot = transform;

        closedRotation = pivot.rotation;
        openRotation = pivot.rotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    public void Interact()
    {

        Debug.Log("Door interacted!");
        isOpen = !isOpen;
    }

    void Update()
    {
        if (isOpen){
            pivot.rotation = Quaternion.Lerp(pivot.rotation, openRotation, Time.deltaTime * openSpeed);
            // Debug.Log("Door rotation: " + pivot.rotation.eulerAngles);
        }
        else{
            pivot.rotation = Quaternion.Lerp(pivot.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
