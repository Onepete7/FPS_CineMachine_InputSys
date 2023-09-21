// using UnityEngine;

// namespace LAMA
// {
//     public class ChasingState : MonsterStateBase
//     {
//         private Transform currentTarget;

//         public ChasingState(bool needsExitTime, LAMA_EnemyClass Monster, Transform Target) : base(needsExitTime, Monster)
//         {
//             this.currentTarget = Target;
//         }

//         public override void OnEnter()
//         {
//             base.OnEnter();
//             MonsterNavMeshAgent.enabled = true;
//             MonsterNavMeshAgent.isStopped = false;
//             MonsterAnimator.Play(stateName: "Walk");
//         }

//         public override void OnLogic()
//         {
//             base.OnLogic();
//             if (!RequestedExit)
//             {
//                 MonsterNavMeshAgent.SetDestination(currentTarget.position);
//             }
//             else if (MonsterNavMeshAgent.remainingDistance <= MonsterNavMeshAgent.stoppingDistance)
//             {
//                 fsm.StateCanExit();
//             }
//         }
//     }
// }