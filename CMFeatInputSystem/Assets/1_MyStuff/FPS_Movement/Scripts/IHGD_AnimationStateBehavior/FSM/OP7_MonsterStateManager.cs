using UnityEngine;
using UnityEngine.AI;

namespace IHGD
{

    public class OP7_MonsterStateManager : MonoBehaviour
    {
        //All the variables we'll need

        Animator monsterAnimator;

        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        public Transform player;

        public LayerMask whatIsGround, whatIsPlayer;
        public NavMeshAgent agent;

        public float timeBetweenAttacks = 2.0f;
        bool alreadyAttacked;

        int numberOfAttacks;

        //State Variable Management
        MonsterBaseState currentState;





        public OP7_MonsterPatrollingState PatrollingState = new OP7_MonsterPatrollingState();
        public OP7_MonsterChasingState ChasingState = new OP7_MonsterChasingState();
        public OP7_MonsterAttackingState AttackingState = new OP7_MonsterAttackingState();




        // private void Awake()
        // {
        //     monsterAnimator = GetComponent<Animator>();
        // }

        // private void Start()
        // {
        //     currentState = PatrollingState;

        //     currentState.EnterState(this);
        // }

        // private void Update()
        // {
        //     currentState.UpdateState(this);
        // }

        // public void SwitchState(MonsterBaseState state)
        // {
        //     currentState = state;
        //     state.EnterState(this);
        // }
    }
}