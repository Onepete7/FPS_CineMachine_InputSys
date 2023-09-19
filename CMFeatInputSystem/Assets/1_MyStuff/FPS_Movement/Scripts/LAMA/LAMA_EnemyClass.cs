using UnityEngine;
using FSM;
using UnityEngine.AI;
using LAMA.Sensors;
using System;

namespace LAMA
{

    [RequireComponent(requiredComponent: typeof(Animator), requiredComponent2: typeof(NavMeshAgent))]

    public class LAMA_EnemyClass : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private OP7_PlayerMovement Player;

        [Header("Attack Config")]
        [SerializeField]
        [Range(0.1f, 5f)]
        private float AttackCooldown = 2;

        [Header("Sensors")]
        [SerializeField]
        private PLAYER_Sensor FollowPlayerSensor;
        [SerializeField]
        private PLAYER_Sensor MeleePlayerSensor;

        [Space]
        [Header("Debug Info")]
        [SerializeField]
        private bool IsInMeleeRange;
        [SerializeField]
        private bool IsInChasingRange;
        [SerializeField]
        private float LastAttackTime;


        private StateMachine<EnumStates, EnumStateEvent> MonsterFSM;
        private Animator MonsterAnimator;
        private NavMeshAgent MonsterNavMeshAgent;

        private void Awake()
        {
            MonsterNavMeshAgent = GetComponent<NavMeshAgent>();
            MonsterAnimator = GetComponent<Animator>();
            MonsterFSM = new StateMachine<EnumStates, EnumStateEvent>();


            //Adding States

            MonsterFSM.AddState(name: EnumStates.Idle, new IdleState(needsExitTime: false, Monster: this));
            // MonsterFSM.AddState(name: EnumStates.Patrolling, new PatrollingState(needsExitTime: true, LAMA_EnemyClass: this));
            MonsterFSM.AddState(name: EnumStates.Chasing, new ChasingState(needsExitTime: true, Monster: this, Player.transform));
            MonsterFSM.AddState(name: EnumStates.Attacking, new AttackingState(needsExitTime: true, Monster: this, OnAttack));

            // Add Transitions
            MonsterFSM.AddTriggerTransition(EnumStateEvent.DetectPlayer, new Transition<EnumStates>(EnumStates.Idle, EnumStates.Chasing));
            MonsterFSM.AddTriggerTransition(EnumStateEvent.LostPlayer, new Transition<EnumStates>(EnumStates.Chasing, EnumStates.Idle));
            MonsterFSM.AddTransition(new Transition<EnumStates>(EnumStates.Idle, EnumStates.Chasing,
                (transition) => IsInChasingRange
                                && Vector3.Distance(Player.transform.position, transform.position) > MonsterNavMeshAgent.stoppingDistance)
            );
            MonsterFSM.AddTransition(new Transition<EnumStates>(EnumStates.Chasing, EnumStates.Idle,
                (transition) => !IsInChasingRange
                                || Vector3.Distance(Player.transform.position, transform.position) <= MonsterNavMeshAgent.stoppingDistance)
            );

            // Attack Transitions
            MonsterFSM.AddTransition(new Transition<EnumStates>(EnumStates.Chasing, EnumStates.Attacking, ShouldMelee, true));
            MonsterFSM.AddTransition(new Transition<EnumStates>(EnumStates.Idle, EnumStates.Attacking, ShouldMelee, true));
            MonsterFSM.AddTransition(new Transition<EnumStates>(EnumStates.Attacking, EnumStates.Chasing, IsNotWithinIdleRange));
            MonsterFSM.AddTransition(new Transition<EnumStates>(EnumStates.Attacking, EnumStates.Idle, IsWithinIdleRange));


            MonsterFSM.SetStartState(name: EnumStates.Idle);

            MonsterFSM.Init();
        }

        private void Start()
        {
            FollowPlayerSensor.OnPlayerEnter += FollowPlayerSensor_OnPlayerEnter;
            FollowPlayerSensor.OnPlayerExit += FollowPlayerSensor_OnPlayerExit;
            MeleePlayerSensor.OnPlayerEnter += MeleePlayerSensor_OnPlayerEnter;
            MeleePlayerSensor.OnPlayerExit += MeleePlayerSensor_OnPlayerExit;

        }



        private void FollowPlayerSensor_OnPlayerEnter(Transform Player)
        {
            MonsterFSM.Trigger(EnumStateEvent.DetectPlayer);
            IsInChasingRange = true;
        }


        private void FollowPlayerSensor_OnPlayerExit(Vector3 LastKnownPosition)
        {
            MonsterFSM.Trigger(EnumStateEvent.LostPlayer);
            IsInChasingRange = false;
        }

        private bool ShouldMelee(Transition<EnumStates> Transition) =>
            LastAttackTime + AttackCooldown <= Time.time
                   && IsInMeleeRange;

        private bool IsWithinIdleRange(Transition<EnumStates> Transition) =>
            MonsterNavMeshAgent.remainingDistance <= MonsterNavMeshAgent.stoppingDistance;

        private bool IsNotWithinIdleRange(Transition<EnumStates> Transition) =>
            !IsWithinIdleRange(Transition);

        private void MeleePlayerSensor_OnPlayerEnter(Transform Player) => IsInMeleeRange = true;


        private void MeleePlayerSensor_OnPlayerExit(Vector3 lastKnownPosition) => IsInMeleeRange = false;








        private void OnAttack(State<EnumStates, EnumStateEvent> State)
        {
            transform.LookAt(Player.transform.position);
            LastAttackTime = Time.time;
        }





        private void Update()
        {
            MonsterFSM.OnLogic();
        }
    }
}

