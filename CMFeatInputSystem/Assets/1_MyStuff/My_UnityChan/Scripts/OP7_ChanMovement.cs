using UnityEngine;
using UnityEngine.InputSystem;

public class OP7_ChanMovement : MonoBehaviour
{
    //NewInputSystem
    UnityChanInputSystem unityChanInputSystem;
    CharacterController unityChanCharacterController;

    //MovementVariables
    Vector2 chanCurrentMovementINPUTVector2;
    Vector3 chanCurrentMovementVector3;
    bool chanIsMovementPressedBool;



    private void Awake()
    {
        unityChanInputSystem = new UnityChanInputSystem();
        unityChanCharacterController = GetComponent<CharacterController>();

        unityChanInputSystem.UnityChanActionMap.UnityChanMoveAction.started += unityChanOnMovementInputFunction;
        unityChanInputSystem.UnityChanActionMap.UnityChanMoveAction.canceled += unityChanOnMovementInputFunction;
        unityChanInputSystem.UnityChanActionMap.UnityChanMoveAction.performed += unityChanOnMovementInputFunction;
    }

    private void Update()
    {
        //"Move" action is from built-in Unity Character Controller
        unityChanCharacterController.Move(chanCurrentMovementVector3 * Time.deltaTime);
    }

    //InputAction.CallbackContext derives from UnityEngine.InputSystem
    void unityChanOnMovementInputFunction(InputAction.CallbackContext unityChanContext)
    {
        chanCurrentMovementINPUTVector2 = unityChanContext.ReadValue<Vector2>();
        chanCurrentMovementVector3.x = chanCurrentMovementINPUTVector2.x;
        chanCurrentMovementVector3.z = chanCurrentMovementINPUTVector2.y;
        chanIsMovementPressedBool = chanCurrentMovementINPUTVector2.x != 0 || chanCurrentMovementINPUTVector2.y != 0;
    }

    private void OnEnable()
    {
        unityChanInputSystem.UnityChanActionMap.Enable();
    }

    private void OnDisable()
    {
        unityChanInputSystem.UnityChanActionMap.Disable();
    }
}
