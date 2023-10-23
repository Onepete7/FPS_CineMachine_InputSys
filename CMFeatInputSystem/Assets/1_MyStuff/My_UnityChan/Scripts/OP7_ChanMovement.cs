using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OP7_ChanMovement : MonoBehaviour
{
    //NewInputSystem
    UnityChanInputManager unityChanInputManager;
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
    bool chanIsJumping = false;
    float chanInitialJumpVelocity;
    float chanMaxJumpHeight = 5.0f;
    float chanMaxJumpTime = 1.0f; //it's in seconds
    int chanIsJumpingHash;
    int chanJumpCountHash;
    bool chanIsJumpAnimating = false;
    int chanJumpCount = 0;
    Coroutine chanCurrentJumpResetRoutine = null; // The Routine is null, there is nothing assigned to it yet





    //using System.Collections.Generic
    Dictionary<int, float> chanInitialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> chanJumpGravities = new Dictionary<int, float>();




    //gravities
    float CHANGROUNDEDGRAVITY = -0.05f;
    float chanFirstJumpGravity = -9.8f;





    //Optimized code
    int chanIsRunningHash;
    int chanIsWalkingHash;


    //CONSTANTS
    float CHANROTATIONPERFRAME = 15.0f;
    float CHANWALKMULTIPLIER = 3.0f;
    float CHANRUNMULTIPLIER = 5.0f;




    private void Awake()
    {
        unityChanInputManager = new UnityChanInputManager();
        unityChanCharacterController = GetComponent<CharacterController>();
        unityChanAnimator = GetComponent<Animator>();

        //We'll look after the PARAMETERS IN THE ANIMATOR
        chanIsWalkingHash = Animator.StringToHash("chanIsWalkingParam");
        chanIsRunningHash = Animator.StringToHash("chanIsRunningParam");
        chanIsJumpingHash = Animator.StringToHash("chanIsJumpingParam");
        chanJumpCountHash = Animator.StringToHash("chanJumpCountParam");

        unityChanInputManager.UnityChanActionMap.UnityChanMoveAction.started += UnityChanOnMovementVector2Function;
        unityChanInputManager.UnityChanActionMap.UnityChanMoveAction.canceled += UnityChanOnMovementVector2Function;
        unityChanInputManager.UnityChanActionMap.UnityChanMoveAction.performed += UnityChanOnMovementVector2Function;
        unityChanInputManager.UnityChanActionMap.UnityChanRunAction.started += UnityChanOnRunButtonFunction;
        unityChanInputManager.UnityChanActionMap.UnityChanRunAction.canceled += UnityChanOnRunButtonFunction;
        unityChanInputManager.UnityChanActionMap.UnityChanJumpAction.started += UnityChanOnJumpButtonFunction;
        unityChanInputManager.UnityChanActionMap.UnityChanJumpAction.canceled += UnityChanOnJumpButtonFunction;

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

        if (chanJumpCount > 0)
        {
            Debug.Log(chanJumpCount);
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
        bool chanIsFalling = chanCurrentWalkMovementVector3.y <= 0.0f || !chanIsJumpPressedBool;
        float chanFallingMultiplier = 2.0f;
        //isGrounded is from the built-in Character Controller
        if (unityChanCharacterController.isGrounded)
        {
            if (chanIsJumpAnimating)
            {
                unityChanAnimator.SetBool(chanIsJumpingHash, false);
                chanIsJumpAnimating = false;
                //We CALL THE FUNCTION/COROUTINE HERE => StartCoroutine(IEnumerator())
                chanCurrentJumpResetRoutine = StartCoroutine(chanJumpResetRoutine()); //If jump is animating (no matter the jump), the chan..Routine is NOT NULL (see lign 251)
                if (chanJumpCount == 3)
                {
                    chanJumpCount = 0;
                    unityChanAnimator.SetInteger(chanJumpCountHash, chanJumpCount);
                }
            }

            chanCurrentWalkMovementVector3.y = CHANGROUNDEDGRAVITY;
            chanCurrentRunMovementVector3.y = CHANGROUNDEDGRAVITY;
        }

        else if (chanIsFalling)
        {
            float previousYVelocity = chanCurrentWalkMovementVector3.y;
            float newYVelocity = chanCurrentWalkMovementVector3.y + (chanJumpGravities[chanJumpCount] * chanFallingMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * .5f, -20.0f);
            chanCurrentWalkMovementVector3.y = nextYVelocity;
            chanCurrentRunMovementVector3.y = nextYVelocity;
        }

        else
        {
            float previousYVelocity = chanCurrentWalkMovementVector3.y;
            float newYVelocity = chanCurrentWalkMovementVector3.y + (chanJumpGravities[chanJumpCount] * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            chanCurrentWalkMovementVector3.y = nextYVelocity;
            chanCurrentRunMovementVector3.y = nextYVelocity;
        }
    }





    void UnityChanHandleJumpVariables()
    {
        float timeToApex = chanMaxJumpTime / 2;

        chanFirstJumpGravity = (-2 * chanMaxJumpHeight) / Mathf.Pow(timeToApex, 2);
        chanInitialJumpVelocity = (2 * chanMaxJumpHeight) / timeToApex;

        float chanSecondJumpGravity = (-2 * (chanMaxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float chanSecondInitialVelocity = (2 * (chanMaxJumpHeight + 2)) / (timeToApex * 1.25f);
        float chanThirdJumpGravity = (-2 * (chanMaxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float chanThirdInitialVelocity = (2 * (chanMaxJumpHeight + 4)) / (timeToApex * 1.5f);

        chanInitialJumpVelocities.Add(1, chanInitialJumpVelocity);
        chanInitialJumpVelocities.Add(2, chanSecondInitialVelocity);
        chanInitialJumpVelocities.Add(3, chanThirdInitialVelocity);

        chanJumpGravities.Add(0, chanFirstJumpGravity); //jumpCountIs0
        chanJumpGravities.Add(1, chanFirstJumpGravity); //jumpCountIs1
        chanJumpGravities.Add(2, chanSecondJumpGravity);
        chanJumpGravities.Add(3, chanThirdJumpGravity);

    }

    void UnityChanHandleJump()
    {
        if (!chanIsJumping && unityChanCharacterController.isGrounded && chanIsJumpPressedBool)
        {
            if (chanJumpCount < 3 && chanCurrentJumpResetRoutine != null) //if we are not at max jump count AND the jump is animating
            {
                StopCoroutine(chanCurrentJumpResetRoutine); //We stop the coroutine, execute all remaingning code, so chanCurrentJumpResetRoutine IS NULL
            }
            unityChanAnimator.SetBool(chanIsJumpingHash, true);
            chanIsJumpAnimating = true;
            chanIsJumping = true;
            chanJumpCount++;
            unityChanAnimator.SetInteger(chanJumpCountHash, chanJumpCount);

            chanCurrentWalkMovementVector3.y = chanInitialJumpVelocities[chanJumpCount] * .5f;
            chanCurrentRunMovementVector3.y = chanInitialJumpVelocities[chanJumpCount] * .5f;
        }

        else if (!chanIsJumpPressedBool && chanIsJumping && unityChanCharacterController.isGrounded)
        {
            chanIsJumping = false;
        }
    }


    //The IEnumerator store a function/CoRoutine that will yield (wait for something) and then do something when the wait time is expired
    IEnumerator chanJumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f); //.5 is half a second
        chanJumpCount = 0;
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
        unityChanInputManager.UnityChanActionMap.Enable();
    }

    private void OnDisable()
    {
        unityChanInputManager.UnityChanActionMap.Disable();
    }
}
