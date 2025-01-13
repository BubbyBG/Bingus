using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /*Need to fix
    Better floor detection (probably use capsule cast instead of raycast)
        gravity currently doesn't stop increasing when on the edge of something
    Vector magnitude capping to avoid diagonal overspeeding
    Snapping to avoid janky downhill bouncing

    //Other to add
    Accleration modifier (air vs ground)
    Crouching
    Slope speed adjustment (speed multiplier using dot product)
    Steep slope sliding (not sure how complicated)
    Sprinting
    Edge jumping

    */
    [SerializeField]
    private LayerMask floorType;
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    private float speedMax;
    [SerializeField]
    private float speedRate;
    [SerializeField]
    private float gravityForce;
    [SerializeField]
    private float jumpForce;
    private float verticalSpeed;
    private float cameraAngle;
    private CharacterController playerController;
    private Vector3 currentVelocity;
    private bool onGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        currentVelocity = new Vector3(0f, 0f, 0f);
        verticalSpeed = 0;
        onGround = false;
        cameraAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float inputForward = Input.GetAxisRaw("Vertical");
        float inputSideways = Input.GetAxisRaw("Horizontal");
        //Create vector from inputs (relative to current position and rotation)
        Vector3 inputDirection = transform.forward * inputForward + transform.right * inputSideways;
        currentVelocity.y = 0; //mask out vertical speed so it doesn't interfere with speed adjustments
        currentVelocity = Vector3.Lerp(currentVelocity, inputDirection * speedMax, speedRate * Time.deltaTime);
        verticalSpeed -= gravityForce * Time.deltaTime; //increase speed due to gravity
        currentVelocity -= Vector3.down * verticalSpeed; //add gravity vector to movement vector
        playerController.Move(currentVelocity * Time.deltaTime); //move player according to current velocity vector
        
        onGround = Physics.Raycast(transform.position, Vector3.down, 1.7f, floorType);
        
        if (onGround)
        {
            //stop gravity acceleration when moving down into the floor
            if (verticalSpeed < 0)
            {
                verticalSpeed = 0;
            }
            //Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
            }
        }

        //Player and camera rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * 500f; //Mouse hor rotation
        float mouseY = Input.GetAxisRaw("Mouse Y") * 500f; //Mouse vert rotation
        cameraAngle -= mouseY * Time.deltaTime;
        cameraAngle = Mathf.Clamp(cameraAngle, -85f, 85f); //Limit angles
        cameraObject.transform.localEulerAngles = new Vector3(cameraAngle, 0f, 0f); //move camera vertically relative to player's transform
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime, Space.Self); //rotate player horizontally
    }
}
