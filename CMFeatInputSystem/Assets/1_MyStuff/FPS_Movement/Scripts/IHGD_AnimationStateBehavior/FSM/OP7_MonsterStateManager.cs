using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace IHGD
{

    public class OP7_MonsterStateManager : MonoBehaviour
    {
        NavMeshAgent monsterNavMeshAgent;



        //ANIMATOR
        Animator monsterAnimator;
        // private static readonly int isPatrollingHash = Animator.StringToHash("Patrolling");
        // private static readonly int isChasingHash = Animator.StringToHash("Chasing");
        // private static readonly int isAttackingHash = Animator.StringToHash("Attacking");
        // bool isPatrolling;
        // bool isChasing;
        // bool isAttacking;




        public Transform playerTransform;

        Transform monsterTransform;

        [SerializeField] Vector3 walkPoint;
        bool walkPointSet;
        [SerializeField] float walkPointRange;
        [SerializeField] float sightRange, attackRange;




        bool playerInSightRange, playerInAttackRange;

        [SerializeField] LayerMask whatIsGround, whatIsPlayer;

        //State Variable Management
        MonsterBaseState monsterCurrentState;
        MonsterStateFactory monsterStates;



        //getters and setters
        public MonsterBaseState MonsterCurrentState { get { return monsterCurrentState; } set { monsterCurrentState = value; } }

        public NavMeshAgent MonsterNavMeshAgent { get { return monsterNavMeshAgent; } }

        public Transform PlayerTransform { get { return playerTransform; } }

        public Animator MonsterAnimator { get { return monsterAnimator; } }
        // public int IsPatrollingHash { get { return isPatrollingHash; } }
        // public int IsChasingHash { get { return isChasingHash; } }
        // public int IsAttackingHash { get { return isAttackingHash; } }
        // public bool IsPatrolling { get { return isPatrolling; } }
        // public bool IsChasing { get { return isChasing; } }
        // public bool IsAttacking { get { return isAttacking; } }
        public Vector3 WalkPoint { get { return walkPoint; } set { walkPoint = value; } }
        public bool WalkPointSet { get { return walkPointSet; } set { walkPointSet = value; } }
        public float WalkPointRange { get { return walkPointRange; } }
        public float SightRange { get { return sightRange; } }
        public float AttackRange { get { return attackRange; } }
        public bool PlayerInSightRange { get { return playerInSightRange; } set { playerInSightRange = value; } }
        public bool PlayerInAttackRange { get { return playerInAttackRange; } set { playerInAttackRange = value; } }
        public LayerMask WhatIsGround { get { return whatIsGround; } }
        public LayerMask WhatIsPlayer { get { return whatIsPlayer; } }
        public Transform MonsterTransform { get { return monsterTransform; } }








        private void Awake()
        {
            //setup animator and NavMeshAgent
            monsterAnimator = GetComponent<Animator>();
            monsterNavMeshAgent = GetComponent<NavMeshAgent>();

            //setup state
            monsterStates = new MonsterStateFactory(this);
            monsterCurrentState = monsterStates.Patrolling();
            monsterCurrentState.EnterState();

            //Find Player
            playerTransform = GameObject.Find("OP7_Player").transform;
            monsterTransform = gameObject.transform;
        }

        private void Update()
        {
            monsterCurrentState.UpdateState();
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}