using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class OP7_PlayerMovementINPTSYS : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField] float fpsMaxMoveSpeedFloat = 7.0f;
    [SerializeField] Transform fpsOrientationTransform;
    Vector3 fpsMoveDirectionVector3;
    Rigidbody fpsRigidBody;

    //JUMP
    [SerializeField] float fpsJumpForceFloat = 12.0f;
    [SerializeField] float fpsJumpCooldownFloat = 0.25f;
    [SerializeField] float fpsAirMultiplierFloat = 0.4f;
    bool fpsIsReadyToJump;





    [Header("Ground Check")]
    [SerializeField] float fpsPlayerHeightFloat = 2.0f;
    [SerializeField] LayerMask fpsWhatIsGroundLayerMask;
    [SerializeField] float fpsGroundDragFloat = 5.0f;
    bool fpsIsGrounded;







    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentSpeedText;







    [Header("NewInputSystem")]

    FPSInputActions fpsInputActions;
    Vector2 fpsCurrentMovementINPUTVector2;

    float fpsHorizontalInputFloat;
    float fpsVerticalInputFloat;

    // float FPS_WALK_MULTIPLIER = 1.0f;
    // float FPS_RUN_MULTIPLIER = 5.0f;

    bool fpsIsMovementPressed;
    bool fpsIsJumpPressed;










    private void Awake()
    {
        fpsInputActions = new FPSInputActions();
        fpsInputActions.FPSActionMap.FPSMovementAction.started += FPSOnMovementInputVector2;
        fpsInputActions.FPSActionMap.FPSMovementAction.canceled += FPSOnMovementInputVector2;
        fpsInputActions.FPSActionMap.FPSMovementAction.performed += FPSOnMovementInputVector2;
        // // fpsInputManager.FPSActionMap.FPSMovementAction.started += UnityChanOnRunButtonFunction;
        // // fpsInputManager.FPSActionMap.FPSMovementAction.canceled += UnityChanOnRunButtonFunction;
        fpsInputActions.FPSActionMap.FPSJumpAction.started += FPSOnJumpButton;
        fpsInputActions.FPSActionMap.FPSJumpAction.canceled += FPSOnJumpButton;
    }

    private void Start()
    {
        fpsRigidBody = GetComponent<Rigidbody>();
        fpsRigidBody.freezeRotation = true;
        fpsIsReadyToJump = true;
    }


    private void Update()
    {
        //ground check
        fpsIsGrounded = Physics.Raycast(transform.position, Vector3.down, fpsPlayerHeightFloat * 0.5f + 0.2f, fpsWhatIsGroundLayerMask);

        FPSInputs();
        SpeedControl();


        //handle Drag
        if (fpsIsGrounded)
            fpsRigidBody.drag = fpsGroundDragFloat;
        else
            fpsRigidBody.drag = 0.0f;


        //handle Text
        if (currentSpeedText != null)
        {
            currentSpeedText.text = "Speed: " + fpsRigidBody.velocity.magnitude.ToString("F2");
        }
    }

    private void FixedUpdate()
    {
        FPSHandleMovement();
    }








    void FPSInputs()
    {
        //whenToJump
        if (fpsIsJumpPressed && fpsIsReadyToJump && fpsIsGrounded)
        {
            fpsIsReadyToJump = false;
            FPSHandleJump();
            Invoke(nameof(FPSHandleResetJump), fpsJumpCooldownFloat);
        }
    }







    private void FPSHandleMovement()
    {
        //calculate movement directionfpsMoveDirectionVector3
        fpsMoveDirectionVector3 = fpsOrientationTransform.forward * fpsVerticalInputFloat + fpsOrientationTransform.right * fpsHorizontalInputFloat;

        //on ground
        if (fpsIsGrounded)
            fpsRigidBody.AddForce(fpsMoveDirectionVector3.normalized * fpsMaxMoveSpeedFloat * 10.0f, ForceMode.Force);

        //in air 
        else if (!fpsIsGrounded)
            fpsRigidBody.AddForce(fpsMoveDirectionVector3.normalized * fpsMaxMoveSpeedFloat * 10.0f * fpsAirMultiplierFloat, ForceMode.Force);
    }






    private void FPSHandleJump()
    {
        // reset y velocity
        fpsRigidBody.velocity = new Vector3(fpsRigidBody.velocity.x, 0.0f, fpsRigidBody.velocity.z);

        fpsRigidBody.AddForce(transform.up * fpsJumpForceFloat, ForceMode.Impulse);
    }

    private void FPSHandleResetJump()
    {
        fpsIsReadyToJump = true;
    }







    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(fpsRigidBody.velocity.x, 0.0f, fpsRigidBody.velocity.z);

        // limit velocity if needed

        if (flatVel.magnitude > fpsMaxMoveSpeedFloat)
        {
            Vector3 limitedVel = flatVel.normalized * fpsMaxMoveSpeedFloat;
            fpsRigidBody.velocity = new Vector3(limitedVel.x, fpsRigidBody.velocity.y, limitedVel.z);
        }
    }








    void FPSOnMovementInputVector2(InputAction.CallbackContext fpsContext)
    {
        fpsCurrentMovementINPUTVector2 = fpsContext.ReadValue<Vector2>();
        fpsHorizontalInputFloat = fpsCurrentMovementINPUTVector2.x; //This is a x (right)
        fpsVerticalInputFloat = fpsCurrentMovementINPUTVector2.y; //This is a z (forward)

        // //Run
        // FPSCurrentRunMovementVector3.x = FPSCurrentMovementINPUTVector2.x * FPS_RUN_MULTIPLIER;
        // FPSCurrentRunMovementVector3.z = FPSCurrentMovementINPUTVector2.y * FPS_RUN_MULTIPLIER;

        fpsIsMovementPressed = fpsCurrentMovementINPUTVector2.x != 0 || fpsCurrentMovementINPUTVector2.y != 0;


        // horizontalInput = Input.GetAxisRaw("Horizontal"); THIS IS KEYBOARD INPUT LEFT/RIGHT
        // verticalInput = Input.GetAxisRaw("Vertical"); THIS IS KEYBOARD INPUT UP/DOWN
    }

    void FPSOnJumpButton(InputAction.CallbackContext fpsContext)
    {
        fpsIsJumpPressed = fpsContext.ReadValueAsButton();
    }





    private void OnEnable()
    {
        fpsInputActions.FPSActionMap.Enable();
    }

    private void OnDisable()
    {
        fpsInputActions.FPSActionMap.Disable();
    }

}
