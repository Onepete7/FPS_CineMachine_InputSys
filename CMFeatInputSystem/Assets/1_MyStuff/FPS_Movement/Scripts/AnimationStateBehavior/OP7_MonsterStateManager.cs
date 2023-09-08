using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OP7_MonsterStateManager : MonoBehaviour
{


    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;


    //Animation
    Animator monsterAnimator;

    Vector3 monsterMovement;
    bool isWalking;
    bool isRunning;
    bool isAttacking;






    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks = 2.0f;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;







    MonsterBaseState currentState;

    public OP7_MonsterPatrollingState PatrollingState = new OP7_MonsterPatrollingState();
    public OP7_MonsterChasingState ChasingState = new OP7_MonsterChasingState();
    public OP7_MonsterAttackingState AttackingState = new OP7_MonsterAttackingState();

    private void Start()
    {
        currentState = PatrollingState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(MonsterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}

