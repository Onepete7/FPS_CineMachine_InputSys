using UnityEngine;
using UnityEngine.AI;

public class OP7_EnemyAiTutorial : MonoBehaviour
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







    private void Awake()
    {
        player = GameObject.Find("OP7_Player").transform;
        agent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }



    }






    private void Patrolling()
    {
        monsterAnimator.SetBool("isWalking", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }







    private void ChasePlayer()
    {
        monsterAnimator.SetBool("isRunning", true);
        agent.SetDestination(player.position);
    }







    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);


        if (!alreadyAttacked)
        {
            monsterAnimator.SetBool("isAttacking", true);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }









    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }








    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }







    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    // private void handleAnimation()
    // {
    //     bool isWalking = monsterAnimator.GetBool("isWalking");
    //     bool isRunning = monsterAnimator.GetBool("isRunning");

    //     transform.hasChanged = false;

    //     if (transform.hasChanged)
    //     {
    //         monsterAnimator.SetBool("isWalking", true);
    //     }
    // }
}





