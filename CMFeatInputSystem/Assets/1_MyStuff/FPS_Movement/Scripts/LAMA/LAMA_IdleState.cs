// using UnityEngine;

// namespace LAMA
// {
//     public class IdleState : MonsterStateBase
//     {
//         public IdleState(bool needsExitTime, LAMA_EnemyClass Monster) : base(needsExitTime, Monster) { }

//         public override void OnEnter()
//         {
//             base.OnEnter();
//             MonsterNavMeshAgent.isStopped = true;
//             MonsterAnimator.Play(stateName: "Idle_A");
//         }
//     }

// }