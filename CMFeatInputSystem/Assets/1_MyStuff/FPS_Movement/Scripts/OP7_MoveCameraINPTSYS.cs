using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OP7_MoveCameraINPTSYS : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
