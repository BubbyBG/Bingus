using UnityEngine;
using System.Collections;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public Transform pivot;
    public float openAngle = 90f;
    public float openSpeed = 100f;
    private bool isOpen = false;
    private bool locked = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Quaternion currentTarget;

    void Awake()
    {
        if (pivot == null) pivot = transform;
    }
    void Start()
    {
        float yAng = pivot.localEulerAngles.y;
        // float dif = Quaternion.Angle(pivot.rotation, Quaternion.identity);
        // Debug.Log("DIF "+dif);
        // int snapped = Mathf.RoundToInt(yAng / 90f) * 90 % 360;

        // if (snapped == 0)
        // // if (Quaternion.Angle(pivot.rotation, Quaternion.identity) > 1f) //already open 
        // {
        //     int snappedY = Mathf.FloorToInt(yAng / 90f) * 90;
        //     Debug.Log("Y: " + yAng);
        //     Debug.Log("Closed: " + snappedY);
        //     closedRotation = Quaternion.Euler(0f, snappedY, 0f);
        // }
        // else
        // {
            closedRotation = pivot.rotation;
        // }
        currentTarget = pivot.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
        // Debug.Log(Quaternion.identity + " vs " + pivot.rotation);
        // if (pivot.rotation != Quaternion.identity){ //(0,0,0)
        //     float angle = transform.localEulerAngles.y;
        //     int snappedY = Mathf.FloorToInt(angle / 90f) * 90;

        //     closedRotation = Quaternion.Euler(0f, snappedY, 0f);
        //     openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
        // }
        // else{
        //     closedRotation = pivot.rotation;
        //     openRotation = pivot.rotation * Quaternion.Euler(0f, openAngle, 0f);

        // }
    }

    public void Interact()
    {
        Debug.Log("Door interacted!");
        float dif = Quaternion.Angle(pivot.rotation, Quaternion.identity);
        Debug.Log("DIF "+dif);
        if(locked){
            StartCoroutine(lockedAnimation());
        }
        else{
            isOpen = !isOpen;
            currentTarget = isOpen ? openRotation : closedRotation;
            Debug.Log("O: " + openRotation);
            Debug.Log("C: " + closedRotation);
            // Quaternion target = isOpen ? openRotation : closedRotation;
            // Debug.Log("it is " + isOpen);

            // pivot.rotation = Quaternion.RotateTowards(
            //     pivot.rotation,
            //     target,
            //     openSpeed * Time.deltaTime * openSpeed
            // ); 
        }
    }

    void Update()
    {
        if (pivot.rotation != currentTarget)
        {
            pivot.rotation = Quaternion.RotateTowards(
                pivot.rotation,
                currentTarget,
                openSpeed * Time.deltaTime * openSpeed
            ); 
        }
    }

    IEnumerator lockedAnimation(){
        Debug.Log("LOCKED");
        float wiggleAngle = 3f;
        float wiggleSpeed = 15f;

        //current rotation
        Quaternion startRot = transform.localRotation;
        //wiggle same direction as open based on sign of openAngle
        Quaternion wiggleRot = startRot * Quaternion.Euler(0f, Mathf.Sign(openAngle) * wiggleAngle, 0f);

        //rotate towards open
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * wiggleSpeed;
            transform.localRotation = Quaternion.Slerp(startRot, wiggleRot, t);
            yield return null;
        }

        //rotate back to original position
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * wiggleSpeed;
            transform.localRotation = Quaternion.Slerp(wiggleRot, startRot, t);
            yield return null;
        }
    }
}
