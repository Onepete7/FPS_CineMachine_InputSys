// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class DummyController : MonoBehaviour
// {
//     DummyControl dummyControl;
//     CharacterController dumbController;

//     Animator dumbAnimator;

//     Vector2 currentMovementInput;
//     Vector3 currentMovement;

//     Vector3 currentRunMovement;

//     public Camera mainCamera;

//     bool isMovementPressed;
//     bool isRunPressed;

//     float walkingSpeed = 10.0f;

//     float runningSpeed = 15.0f;

//     float turningSpeed = 100.0f;
//     int isRunningHash;

//     int isWalkingHash;


//     private void Awake()
//     {
//         dummyControl = new DummyControl();
//         dumbController = GetComponent<CharacterController>();
//         dumbAnimator = GetComponent<Animator>();


//         isWalkingHash = Animator.StringToHash("isWalking");
//         isRunningHash = Animator.StringToHash("isRunning");

//         dummyControl.DumbBoyControl.Move.started += onMovementInput;
//         dummyControl.DumbBoyControl.Move.performed += onMovementInput;
//         dummyControl.DumbBoyControl.Move.canceled += onMovementInput;
//         dummyControl.DumbBoyControl.Run.performed += onRun;
//         dummyControl.DumbBoyControl.Run.canceled += onRun;

//     }


//     void onMovementInput(InputAction.CallbackContext context)
//     {
//         currentMovementInput = context.ReadValue<Vector2>();
//         currentMovement.x = currentMovementInput.x * walkingSpeed;
//         currentMovement.z = currentMovementInput.y * walkingSpeed;
//         currentRunMovement.x = currentMovementInput.x * runningSpeed;
//         currentRunMovement.z = currentMovementInput.y * runningSpeed;
//         isMovementPressed = currentMovementInput.x != 0 | currentMovementInput.y != 0;
//     }

//     void onRun(InputAction.CallbackContext context)
//     {
//         isRunPressed = context.ReadValueAsButton();
//     }

//     void handleAnimation()
//     {
//         bool isWalking = dumbAnimator.GetBool(isWalkingHash);
//         bool isRunning = dumbAnimator.GetBool(isRunningHash);

//         if (isMovementPressed && !isWalking)
//         {
//             dumbAnimator.SetBool(isWalkingHash, true);
//         }

//         else if (!isMovementPressed && isWalking)
//         {
//             dumbAnimator.SetBool(isWalkingHash, false);
//         }

//         if ((isMovementPressed && isRunPressed) && !isRunning)
//         {
//             dumbAnimator.SetBool(isRunningHash, true);
//         }

//         else if ((!isMovementPressed || !isRunPressed) && isRunning)
//         {
//             dumbAnimator.SetBool(isRunningHash, false);
//         }
//     }

//     void handleRotation()
//     {
//         Vector3 positionToLookAt;
//         positionToLookAt.x = currentMovement.x;
//         positionToLookAt.y = 0.0f;
//         positionToLookAt.z = currentMovement.z;

//         Quaternion currentRotation = transform.rotation;


//         if (isMovementPressed)
//         {
//             Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
//             transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, turningSpeed * Time.deltaTime);
//         }
//     }

//     void MovePlayerRelativeToCamera()
//     {
//         Vector3 forward = mainCamera.transform.forward;
//         Vector3 right = mainCamera.transform.right;

//         Vector3 forwardRelativeVerticalInput = currentMovement.z * forward;
//         Vector3 rightRelativeHorizontalInput = currentMovement.x * right;
//         forward.y = 0;
//         right.y = 0;
//         forward = forward.normalized;
//         right = right.normalized;

//         Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
//         this.transform.Translate(cameraRelativeMovement, Space.World);
//     }

//     private void Update()
//     {
//         handleAnimation();
//         handleRotation();
//         MovePlayerRelativeToCamera();

//         if (isRunPressed)
//         {
//             dumbController.Move(currentRunMovement * Time.deltaTime);
//         }
//         else
//         {
//             dumbController.Move(currentMovement * Time.deltaTime);
//         }
//     }

//     private void OnEnable()
//     {
//         dummyControl.DumbBoyControl.Enable();
//     }

//     private void OnDisable()
//     {
//         dummyControl.DumbBoyControl.Disable();
//     }

// }
