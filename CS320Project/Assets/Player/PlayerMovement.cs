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
    private PlayerInput input;
    [SerializeField]
    private LayerMask floorType;
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    public float speedMax;
    [SerializeField]
    private float speedRate;
    [SerializeField]
    private float gravityForce;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float verticalSpeed;
    private float cameraAngle;
    private CharacterController playerController;
    public Vector3 currentVelocity;
    [SerializeField]
    private bool onGround;
    private Vector3 playerBottom;
    private Vector3 playerTop;
    [SerializeField]
    private bool hasJumped; //has recently jumped
    float jumpTimer; //
    private float snapDistance; //max distance to magnetise to floor
    public LayerMask noCollideWith;
    public float inputForward, inputSideways;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        playerController = GetComponent<CharacterController>();
        currentVelocity = new Vector3(0f, 0f, 0f);
        playerBottom = new Vector3(0f, 0f, 0f);
        playerTop = new Vector3(0f, 0f, 0f);
        verticalSpeed = 0;
        onGround = false;
        cameraAngle = 0f;
        hasJumped = true;
        jumpTimer = 0f;
        snapDistance = 0.1f;
        playerController.excludeLayers = noCollideWith; //LayerMask.GetMask("Interactive");
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX, mouseY;
        bool inputJump;
        
        if (!input.inventoryIsOpen)
        {
            //inputForward = Input.GetAxisRaw("Vertical");
            //inputSideways = Input.GetAxisRaw("Horizontal");
            inputJump = Input.GetKeyDown(KeyCode.Space);
            mouseX = Input.GetAxisRaw("Mouse X") * 500f; //Mouse hor rotation
            mouseY = Input.GetAxisRaw("Mouse Y") * 500f; //Mouse vert rotation
        }
        else
        {
            inputForward = 0f;
            inputSideways = 0f;
            inputJump = false;
            mouseX = 0f;
            mouseY = 0f;
        }
        
        //Create vector from inputs (relative to current position and rotation)
        Vector3 inputDirection = transform.forward * inputForward + transform.right * inputSideways;
        currentVelocity.y = 0; //mask out vertical speed so it doesn't interfere with speed adjustments
        currentVelocity = Vector3.Lerp(currentVelocity, inputDirection * speedMax, speedRate * Time.deltaTime);
        currentVelocity -= Vector3.down * verticalSpeed; //add gravity vector to movement vector
        

        //----------------- Floor detection ----------------\\
        playerBottom = transform.position + transform.up * playerController.radius;
        playerTop = transform.position + (transform.up * (playerController.height - playerController.radius));
        onGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, snapDistance + playerController.height / 2,
            -1, QueryTriggerInteraction.Ignore);
        //Physics.CapsuleCast(playerBottom, playerTop, playerController.radius, Vector3.down, out RaycastHit hit, 0.5f, -1, QueryTriggerInteraction.Ignore);
        
        if (onGround)
        {
            //Jumping
            if (inputJump)
            {
                verticalSpeed = jumpForce;
                hasJumped = true;
                jumpTimer = Time.time;
            }

            if (!hasJumped)// && hit.distance < (snapDistance + playerController.height / 2))
            {
                playerController.Move(Vector3.down * hit.distance);
                onGround = true;
                verticalSpeed = 0;
                if (Mathf.Abs(currentVelocity.y) > 0.1f)
                {
                    currentVelocity = Vector3.ProjectOnPlane(currentVelocity, hit.normal); //adjust speed according to hit surface normal direction
                }
            }
        }
        else
        {
            verticalSpeed -= gravityForce * Time.deltaTime; //increase speed due to gravity
        }

        /*if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit2, snapDistance + playerController.height / 2,
                -1, QueryTriggerInteraction.Ignore))
            //(Physics.CapsuleCast(playerBottom, playerTop, playerController.radius, currentVelocity.normalized, out RaycastHit hit2, currentVelocity.magnitude * Time.deltaTime, -1, QueryTriggerInteraction.Ignore))
        {
            currentVelocity = Vector3.ProjectOnPlane(currentVelocity, hit2.normal); //adjust speed according to hit surface normal direction
            playerController.Move(Vector3.down * hit2.distance);
            onGround = true;
            verticalSpeed = 0;
        }*/
        
        if ((Time.time - jumpTimer) > 0.25f) //if it's been a certain time since last jump
        {
            hasJumped = false;
        }

        //vertical speed cancellation for floors
        

        playerController.Move(currentVelocity * Time.deltaTime); //move player according to current velocity vector

        //Player and camera rotation
        cameraAngle -= mouseY * Time.deltaTime;
        cameraAngle = Mathf.Clamp(cameraAngle, -85f, 85f); //Limit angles
        cameraObject.transform.localEulerAngles = new Vector3(cameraAngle, 0f, 0f); //move camera vertically relative to player's transform
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime, Space.Self); //rotate player horizontally
    }
}
