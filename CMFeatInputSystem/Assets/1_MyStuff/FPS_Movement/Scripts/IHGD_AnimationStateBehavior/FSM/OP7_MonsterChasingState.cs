namespace IHGD
{
    using UnityEngine;

    public class OP7_MonsterChasingState : MonsterBaseState
    {
        public OP7_MonsterChasingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }

        public override void EnterState()
        {
            ctx.MonsterAnimator.SetBool("isChasing", true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
            ChasePlayer();
        }

        public override void ExitState()
        {

        }

        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            ctx.PlayerInSightRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.SightRange, ctx.WhatIsPlayer);
            ctx.PlayerInAttackRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.AttackRange, ctx.WhatIsPlayer);

            if (!ctx.PlayerInSightRange && !ctx.PlayerInAttackRange)
            {
                SwitchState(factory.Patrolling());
                Debug.Log("ChasingToPatrolling");
            }

            else if (ctx.PlayerInSightRange && ctx.PlayerInAttackRange)
            {
                SwitchState(factory.Attacking());
                Debug.Log("ChasingToAttacking");
            }
        }



        private void ChasePlayer()
        {
            ctx.MonsterAnimator.SetBool("isChasing", true);
            ctx.MonsterNavMeshAgent.SetDestination(ctx.PlayerTransform.position);
        }

    }
}