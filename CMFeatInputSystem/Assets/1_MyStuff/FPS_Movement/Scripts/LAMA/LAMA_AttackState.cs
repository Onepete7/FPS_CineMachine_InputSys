using FSM;
using System;

namespace LAMA
{
    public class AttackingState : MonsterStateBase
    {
        public AttackingState(
            bool needsExitTime,
            LAMA_EnemyClass Monster,
            Action<State<EnumStates, EnumStateEvent>> onEnter,
            float ExitTime = 0.33f) : base(needsExitTime, Monster, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            MonsterNavMeshAgent.isStopped = true;
            base.OnEnter();
            MonsterAnimator.Play(stateName: "Attack");
        }
    }
}