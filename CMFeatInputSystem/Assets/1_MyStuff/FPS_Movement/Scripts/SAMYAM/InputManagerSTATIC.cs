// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class InputManagerSTATIC : MonoBehaviour
// {
//     static InputManagerSTATIC _fpsInputManagerSTATICInstance; //this one private

//     public static InputManagerSTATIC Instance //this one public, for the get function
//     {
//         get
//         {
//             return _fpsInputManagerSTATICInstance;
//         }
//     }

//     FPSInputActions fpsInputActions;







//     void Awake()
//     {
//         if (_fpsInputManagerSTATICInstance != null && _fpsInputManagerSTATICInstance != this)
//         {
//             Destroy(this.gameObject);
//         }
//         else
//         {
//             _fpsInputManagerSTATICInstance = this;
//         }

//         fpsInputActions = new FPSInputActions();
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }


//     public Vector2 FPSGetPlayerMovement()
//     {
//         return fpsInputActions.FPSActionMap.FPSMovementAction.ReadValue<Vector2>();
//     }

//     public Vector2 FPSGetMouseDelta()
//     {
//         return fpsInputActions.FPSActionMap.FPSLookAction.ReadValue<Vector2>();
//     }

//     public bool FPSPlayerJumpedThisFrame()
//     {
//         return fpsInputActions.FPSActionMap.FPSJumpAction.triggered; //Returns true if the control was triggered on that frame
//     }

//     void OnEnable()
//     {
//         fpsInputActions.Enable();
//     }

//     void OnDisable()
//     {
//         fpsInputActions.Disable();
//     }
// }
