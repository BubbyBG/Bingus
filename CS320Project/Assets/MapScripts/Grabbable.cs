using UnityEngine;

public class Grabable: MonoBehaviour, IInteractable {
    public Transform holdPoint;
    private Rigidbody rb;
    private bool isHeld;

    void Start(){
        rb = GetComponent<Rigidbody>();
        isHeld = false;
    }

    public void Interact(){
        if(isHeld){
            Release();
        }
        else{
            tryGrab();
        }
    }


    private void Release(){
        isHeld = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().linearDamping = 0f;
    }

    private void tryGrab(){
        isHeld = true;
        rb.useGravity = false;
        rb.linearDamping = 10f;
    }

    public void Update(){
        if(isHeld && holdPoint != null){
            Vector3 dir = (holdPoint.position - transform.position);
            rb.linearVelocity = dir * 15f;
        }
    }


}