using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Limit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

}
