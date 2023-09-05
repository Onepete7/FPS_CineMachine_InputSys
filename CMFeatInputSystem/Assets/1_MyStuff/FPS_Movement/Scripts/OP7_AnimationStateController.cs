using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OP7_AnimationStateController : MonoBehaviour
{
    public Animator monsterAnimator;
    private void Start()
    {
        monsterAnimator = GetComponent<Animator>();
    }
}
