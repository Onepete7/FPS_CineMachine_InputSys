using UnityEngine;
using UnityEngine.InputSystem;

public class OP7_PlayerCamINPTSYS : MonoBehaviour
{

    //Camera
    [SerializeField] float sensX = 1;
    [SerializeField] float sensY = 1;

    [SerializeField] Transform orientation;

    //Rotation
    float xRotation;
    float yRotation;

    //Inputs
    FPSInputActions fpsInputActions;
    Vector2 fpsMouseDeltaVector2;


    void Awake()
    {
        fpsInputActions = new FPSInputActions();

        fpsInputActions.FPSActionMap.FPSLookAction.started += FPSOnLookVector2Function;
        fpsInputActions.FPSActionMap.FPSLookAction.canceled += FPSOnLookVector2Function;
        fpsInputActions.FPSActionMap.FPSLookAction.performed += FPSOnLookVector2Function;
    }




    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }





    void Update()
    {
        // float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        // float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        float mouseX = fpsMouseDeltaVector2.x * Time.deltaTime * sensX;
        float mouseY = fpsMouseDeltaVector2.y * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    void FPSOnLookVector2Function(InputAction.CallbackContext fpsContext)
    {
        fpsMouseDeltaVector2 = fpsContext.ReadValue<Vector2>();
    }


    private void OnEnable()
    {
        fpsInputActions.Enable();
    }

    private void OnDisable()
    {
        fpsInputActions.Enable();
    }
}
