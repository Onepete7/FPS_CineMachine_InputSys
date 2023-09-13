using UnityEngine;
using FSM;
using UnityEngine.AI;

namespace LAMA
{

    [RequireComponent(requiredComponent: typeof(Animator), requiredComponent2: typeof(NavMeshAgent))]

    public class LAMA_EnemyClass : MonoBehaviour
    {
        private StateMachine<EnumStates, EnumStateEvent> MonsterFSM;
        private Animator MonsterAnimator;
        private NavMeshAgent MonsterNavMeshAgent;

        private void Awake()
        {
            MonsterNavMeshAgent = GetComponent<NavMeshAgent>();
            MonsterAnimator = GetComponent<Animator>();
            MonsterFSM = new StateMachine<EnumStates, EnumStateEvent>();

            // MonsterFSM.AddState(name: EnumStates.Idle, new IdleState(needsExitTime: false, LAMA_EnemyClass: this));
            // MonsterFSM.AddState(name: EnumStates.Patrolling, new PatrollingState(needsExitTime: true, LAMA_EnemyClass: this));
            // MonsterFSM.AddState(name: EnumStates.Chasing, new ChasingState(needsExitTime: true, LAMA_EnemyClass: this));
            // MonsterFSM.AddState(name: EnumStates.Attacking, new AttackingState(needsExitTime: true, LAMA_EnemyClass: this));

            MonsterFSM.SetStartState(name: EnumStates.Idle);

            MonsterFSM.Init();
        }

        private void Update()
        {
            MonsterFSM.OnLogic();
        }
    }
}

