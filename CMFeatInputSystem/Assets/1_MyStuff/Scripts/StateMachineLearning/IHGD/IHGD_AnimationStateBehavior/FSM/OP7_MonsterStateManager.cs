using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEditor;

namespace IHGD
{

    public class OP7_MonsterStateManager : MonoBehaviour
    {

        //NavMeshAgent
        NavMeshAgent monsterNavMeshAgent;



        //ANIMATOR
        Animator monsterAnimator;
        string currentAnimationState;
        string mONSTER_PATROLLING = "Monster_Patrolling";
        string mONSTER_CHASING = "Monster_Chasing";
        string mONSTER_ATTACKING = "Monster_Attacking";


        //Useful Transforms
        public Transform playerTransform;

        Transform monsterTransform;

        //CoRoutine? Plutot des bools en fait
        Coroutine waitTillAttackDone;



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
        public Vector3 WalkPoint { get { return walkPoint; } set { walkPoint = value; } }
        public bool WalkPointSet { get { return walkPointSet; } set { walkPointSet = value; } }
        public float WalkPointRange { get { return walkPointRange; } }
        public float SightRange { get { return sightRange; } }
        public float AttackRange { get { return attackRange; } }
        public bool PlayerInSightRange { get { return playerInSightRange; } set { playerInSightRange = value; } }
        public bool PlayerInAttackRange { get { return playerInAttackRange; } set { playerInAttackRange = value; } }
        public LayerMask WhatIsGround { get { return whatIsGround; } }
        public LayerMask WhatIsPlayer { get { return whatIsPlayer; } }


        public Transform PlayerTransform { get { return playerTransform; } }
        public Transform MonsterTransform { get { return monsterTransform; } }


        public Animator MonsterAnimator { get { return monsterAnimator; } }
        public string CurrentAnimationState { get { return currentAnimationState; } set { currentAnimationState = value; } }
        public string MONSTER_PATROLLING { get { return mONSTER_PATROLLING; } }
        public string MONSTER_CHASING { get { return mONSTER_CHASING; } }
        public string MONSTER_ATTACKING { get { return mONSTER_ATTACKING; } }


        public Coroutine WaitTillAttackDone { get { return waitTillAttackDone; } }










        private void Awake()
        {
            //setup animator
            monsterAnimator = GetComponent<Animator>();



            //setup NavMesh
            monsterNavMeshAgent = GetComponent<NavMeshAgent>();

            //setup state
            monsterStates = new MonsterStateFactory(this);
            monsterCurrentState = monsterStates.Patrolling();
            monsterCurrentState.EnterState();

            //Find Player&Monster
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






        void ChangeAnimationState(string newAnimationState)
        {
            if (newAnimationState == currentAnimationState)
            {
                return;
            }

            monsterAnimator.Play(newAnimationState);
            currentAnimationState = newAnimationState;
        }

        bool IsAnimationPlaying(Animator animator, string animationStateName)
        {
            if (monsterAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName) &&
                monsterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}