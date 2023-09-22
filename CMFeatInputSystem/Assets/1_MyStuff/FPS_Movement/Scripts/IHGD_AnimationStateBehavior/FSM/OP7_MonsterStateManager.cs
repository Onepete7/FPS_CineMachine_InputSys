using UnityEngine;
using UnityEngine.AI;

namespace IHGD
{

    public class OP7_MonsterStateManager : MonoBehaviour
    {
        //All the variables we'll need

        Animator monsterAnimator;

        public Transform playerTransform;

        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;
        public bool isPlayerBeingChased;

        public bool chased;

        public Transform player;

        public LayerMask whatIsGround, whatIsPlayer;
        public NavMeshAgent agent;

        public float timeBetweenAttacks = 2.0f;
        bool alreadyAttacked;

        int numberOfAttacks;



        //State Variable Management
        MonsterBaseState monsterCurrentState;
        MonsterStateFactory monsterStates;

        //getters and setters
        public MonsterBaseState MonsterCurrentState { get { return monsterCurrentState; } set { monsterCurrentState = value; } }
        public bool IsPlayerCompromised { get { return isPlayerBeingChased; } }




        // public OP7_MonsterPatrollingState PatrollingState = new OP7_MonsterPatrollingState();
        // public OP7_MonsterChasingState ChasingState = new OP7_MonsterChasingState();
        // public OP7_MonsterAttackingState AttackingState = new OP7_MonsterAttackingState();




        private void Awake()
        {
            //setup animator
            monsterAnimator = GetComponent<Animator>();

            //setup state
            monsterStates = new MonsterStateFactory(this);
            monsterCurrentState = monsterStates.Patrolling();
            monsterCurrentState.EnterState();

            //setup detection
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        }
        // private void Start()
        // {
        //     monsterCurrentState = PatrollingState;

        //     monsterCurrentState.EnterState(this);
        // }

        private void Update()
        {
            if (playerInSightRange && !playerInAttackRange)
            {
                isPlayerBeingChased = true;
            }

            monsterCurrentState.UpdateState();
        }

        // public void SwitchState(MonsterBaseState state)
        // {
        //     monsterCurrentState = state;
        //     state.EnterState(this);
        // }
        // }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }






    }


}