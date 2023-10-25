// using UnityEngine;
// using Cinemachine;

// public class OP7_CinemachinePOVExtension : CinemachineExtension
// {
//     [SerializeField] float fpsClampAngleFloat = 80.0f;
//     [SerializeField] float fpsHorizontalSpeed = 10.0f;
//     [SerializeField] float fpsVerticalSpeed = 10.0f;



//     private InputManagerSTATIC inputManagerStatic; //calling the public getter of the InputManagerStatic to we can get the function of the new input sys
//     Vector3 fpsStartingRotationVector3;

//     protected override void Awake()
//     {
//         inputManagerStatic = InputManagerSTATIC.Instance;
//         base.Awake();
//     }

//     protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase OPvcam, CinemachineCore.Stage OPStage, ref CameraState OPState, float OPdeltatime)
//     {
//         if (OPvcam.Follow)
//         {
//             if (OPStage == CinemachineCore.Stage.Aim)
//             {
//                 if (fpsStartingRotationVector3 == null)
//                 {
//                     fpsStartingRotationVector3 = transform.localRotation.eulerAngles;
//                     Vector2 deltaInput = inputManagerStatic.FPSGetMouseDelta();
//                     fpsStartingRotationVector3.x += deltaInput.x * fpsVerticalSpeed * Time.deltaTime;
//                     fpsStartingRotationVector3.y += deltaInput.y * fpsHorizontalSpeed * Time.deltaTime;
//                     fpsStartingRotationVector3.y = Mathf.Clamp(fpsStartingRotationVector3.y, -fpsClampAngleFloat, fpsClampAngleFloat);
//                     OPState.RawOrientation = Quaternion.Euler(fpsStartingRotationVector3.y, fpsStartingRotationVector3.x, 0f);

//                 }
//             }
//         }
//     }


// }