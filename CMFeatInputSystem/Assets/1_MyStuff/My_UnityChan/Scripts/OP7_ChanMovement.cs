using UnityEngine;
using UnityEngine.InputSystem;

public class OP7_ChanMovement : MonoBehaviour
{
    //NewInputSystem
    UnityChanInputSystem unityChanInputSystem;
    //Built-In Unity Animator Controller
    CharacterController unityChanCharacterController;
    //Animator
    Animator unityChanAnimator;



    //Player Input Values and Movement
    Vector2 chanCurrentMovementINPUTVector2;
    Vector3 chanCurrentWalkMovementVector3;
    Vector3 chanCurrentRunMovementVector3;
    bool chanIsMovementPressedBool; //input dependant
    bool chanIsRunPressedBool;  //input dependant


    //Jumping variables
    bool chanIsJumpPressedBool = false;
    bool chanIsJumpingBool = false;
    float chanInitialJumpVelocity;
    public float chanMaxJumpHeight = 500.0f;
    public float chanMaxJumpTime = 150.0f; //it's in seconds
    public float chanFallingMultiplier = 2000.0f;
    bool chanIsFalling = false;

    //gravities
    float CHANGROUNDEDGRAVITY = -0.05f;
    float chanAirGravity = -9.8f;





    //Optimized code
    int chanIsRunningHash;
    int chanIsWalkingHash;


    //CONSTANTS
    float CHANROTATIONPERFRAME = 15f;
    float CHANWALKMULTIPLIER = 3.0f;
    float CHANRUNMULTIPLIER = 15.0f;
    int ZERO = 0;




    private void Awake()
    {
        unityChanInputSystem = new UnityChanInputSystem();
        unityChanCharacterController = GetComponent<CharacterController>();
        unityChanAnimator = GetComponent<Animator>();

        //We'll look after the PARAMETERS IN THE ANIMATOR
        chanIsWalkingHash = Animator.StringToHash("chanIsWalking");
        chanIsRunningHash = Animator.StringToHash("chanIsRunning");

        unityChanInputSystem.UnityChanActionMap.UnityChanMoveAction.started += UnityChanOnMovementVector2Function;
        unityChanInputSystem.UnityChanActionMap.UnityChanMoveAction.canceled += UnityChanOnMovementVector2Function;
        unityChanInputSystem.UnityChanActionMap.UnityChanMoveAction.performed += UnityChanOnMovementVector2Function;
        unityChanInputSystem.UnityChanActionMap.UnityChanRunAction.started += UnityChanOnRunButtonFunction;
        unityChanInputSystem.UnityChanActionMap.UnityChanRunAction.canceled += UnityChanOnRunButtonFunction;
        unityChanInputSystem.UnityChanActionMap.UnityChanJumpAction.started += UnityChanOnJumpButtonFunction;
        unityChanInputSystem.UnityChanActionMap.UnityChanJumpAction.canceled += UnityChanOnJumpButtonFunction;

        UnityChanHandleJumpVariables();
    }


    private void Update()
    {
        UnityChanHandleRotation();
        UnityChanHandleAnimation();
        //"Move" action is from built-in Unity Character Controller

        if (chanIsRunPressedBool)
        {
            unityChanCharacterController.Move(chanCurrentRunMovementVector3 * Time.deltaTime);
        }
        else
        {
            unityChanCharacterController.Move(chanCurrentWalkMovementVector3 * Time.deltaTime);
        }



        UnityChanHandleGravity();
        UnityChanHandleJump();
        if (chanIsFalling)
        {
            Debug.Log(chanIsFalling);
        }
    }

    //InputAction.CallbackContext derives from UnityEngine.InputSystem
    void UnityChanOnMovementVector2Function(InputAction.CallbackContext unityChanContext)
    {
        chanCurrentMovementINPUTVector2 = unityChanContext.ReadValue<Vector2>();
        //Walk
        chanCurrentWalkMovementVector3.x = chanCurrentMovementINPUTVector2.x * CHANWALKMULTIPLIER;
        chanCurrentWalkMovementVector3.z = chanCurrentMovementINPUTVector2.y * CHANWALKMULTIPLIER;
        //Run
        chanCurrentRunMovementVector3.x = chanCurrentMovementINPUTVector2.x * CHANRUNMULTIPLIER;
        chanCurrentRunMovementVector3.z = chanCurrentMovementINPUTVector2.y * CHANRUNMULTIPLIER;

        chanIsMovementPressedBool = chanCurrentMovementINPUTVector2.x != 0 || chanCurrentMovementINPUTVector2.y != 0;
    }

    void UnityChanOnRunButtonFunction(InputAction.CallbackContext unityChanContext)
    {
        chanIsRunPressedBool = unityChanContext.ReadValueAsButton();
    }

    void UnityChanOnJumpButtonFunction(InputAction.CallbackContext unityChanContext)
    {
        chanIsJumpPressedBool = unityChanContext.ReadValueAsButton();
    }











    void UnityChanHandleAnimation()
    {
        bool chanIsWalkingBool = unityChanAnimator.GetBool(chanIsWalkingHash);
        bool chanIsRunningBool = unityChanAnimator.GetBool(chanIsRunningHash);

        if (chanIsMovementPressedBool && !chanIsWalkingBool)
        {
            unityChanAnimator.SetBool(chanIsWalkingHash, true);
        }

        else if (!chanIsMovementPressedBool && chanIsWalkingBool)
        {
            unityChanAnimator.SetBool(chanIsWalkingHash, false);
        }

        if ((chanIsMovementPressedBool && chanIsRunPressedBool) && !chanIsRunningBool)
        {
            unityChanAnimator.SetBool(chanIsRunningHash, true);
        }

        else if ((!chanIsMovementPressedBool || !chanIsRunPressedBool) && chanIsRunningBool)
        {
            unityChanAnimator.SetBool(chanIsRunningHash, false);
        }
    }

    void UnityChanHandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = chanCurrentMovementINPUTVector2.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = chanCurrentMovementINPUTVector2.y;


        Quaternion chanCurrentRotation = transform.rotation;

        if (chanIsMovementPressedBool)
        {
            Quaternion chanTargetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(chanCurrentRotation, chanTargetRotation, CHANROTATIONPERFRAME * Time.deltaTime);
        }
    }

    void UnityChanHandleGravity()
    {
        chanIsFalling = chanCurrentWalkMovementVector3.y <= 0.0f || !chanIsJumpPressedBool; //isFalling is true if the y value is below 0 or equal to
        //isGrounded is from the built-in Character Controller
        if (unityChanCharacterController.isGrounded)
        {
            chanCurrentWalkMovementVector3.y = CHANGROUNDEDGRAVITY;
            chanCurrentRunMovementVector3.y = CHANGROUNDEDGRAVITY;
        }
        else if (chanIsFalling)
        {
            float previousYVelocity = chanCurrentWalkMovementVector3.y;
            float newYVelocity = chanCurrentWalkMovementVector3.y + (chanAirGravity * chanFallingMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            chanCurrentWalkMovementVector3.y += chanAirGravity;
            chanCurrentRunMovementVector3.y += chanAirGravity;
        }
        else
        {
            float previousYVelocity = chanCurrentWalkMovementVector3.y;
            float newYVelocity = chanCurrentWalkMovementVector3.y + (chanAirGravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            chanCurrentWalkMovementVector3.y += chanAirGravity;
            chanCurrentRunMovementVector3.y += chanAirGravity;
        }
    }


    void UnityChanHandleJumpVariables()
    {
        float timeToApex = chanMaxJumpTime / 2;
        chanAirGravity = (-2 * chanMaxJumpHeight) / Mathf.Pow(timeToApex, 2); //Pow is power
        chanInitialJumpVelocity = (2 * chanMaxJumpHeight) / timeToApex;
    }

    void UnityChanHandleJump()
    {
        if (!chanIsJumpingBool && unityChanCharacterController.isGrounded && chanIsJumpPressedBool)
        {
            chanIsJumpingBool = true;
            chanCurrentWalkMovementVector3.y = chanInitialJumpVelocity * .5f;
            chanCurrentRunMovementVector3.y = chanInitialJumpVelocity * .5f;
        }

        else if (!chanIsJumpPressedBool && chanIsJumpingBool && unityChanCharacterController.isGrounded)
        {
            chanIsJumpingBool = false;
        }
    }






    // Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    // {
    //     float currentYValue = vectorToRotate.y;
    //     Vector3 cameraForward = Camera.main.transform.forward;
    //     Vector3 cameraRight = Camera.main.transform.right;

    //     cameraForward.y = 0;
    //     cameraRight.y = 0;

    //     cameraForward = cameraForward.normalized;
    //     cameraForward = cameraRight.normalized;

    //     Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
    //     Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

    //     Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
    //     vectorRotatedToCameraSpace.y = currentYValue;
    //     return vectorRotatedToCameraSpace;
    // }








    private void OnEnable()
    {
        unityChanInputSystem.UnityChanActionMap.Enable();
    }

    private void OnDisable()
    {
        unityChanInputSystem.UnityChanActionMap.Disable();
    }
}
