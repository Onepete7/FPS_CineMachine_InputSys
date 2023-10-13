using UnityEngine;

public class OP7_ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerOrientationTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerCapsuleTransform;
    [SerializeField] Rigidbody playerRigidbody;

    [SerializeField] float rotationSpeedOfPlayer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
        playerOrientationTransform.forward = viewDir.normalized;

        //rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = playerOrientationTransform.forward * verticalInput + playerOrientationTransform.right * horizontalInput;

        if (inputDir != Vector3.zero)
            playerCapsuleTransform.forward = Vector3.Slerp(playerCapsuleTransform.forward, inputDir.normalized, Time.deltaTime * rotationSpeedOfPlayer);

    }
}
