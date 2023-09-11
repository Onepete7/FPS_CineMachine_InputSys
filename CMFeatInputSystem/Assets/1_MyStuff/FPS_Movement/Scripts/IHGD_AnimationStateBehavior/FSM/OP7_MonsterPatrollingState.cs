using UnityEngine.AI;
using UnityEngine;

public class OP7_MonsterPatrollingState : MonsterBaseState
{
    Animator monsterAnimator;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public NavMeshAgent agent;







    public override void EnterState(OP7_MonsterStateManager monster)
    {
        Debug.Log("Come out, come out, wherever you are...");
        playerInSightRange = Physics.CheckSphere(monster.transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(monster.transform.position, attackRange, whatIsPlayer);


    }

    public override void UpdateState(OP7_MonsterStateManager monster)
    {
        void Patrolling()
        {
            if (!walkPointSet)
                SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = monster.transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(monster.transform.position.x + randomX, monster.transform.position.y, monster.transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -monster.transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }


        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        else if (playerInSightRange && !playerInAttackRange)
        {
            monster.SwitchState(monster.ChasingState);
        }
    }

}