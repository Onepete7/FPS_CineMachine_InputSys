using UnityEngine;


namespace IHGD
{


    public class OP7_MonsterAttackingState : MonsterBaseState
    {
        public OP7_MonsterAttackingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }

        public override void EnterState()
        {
            ctx.MonsterAnimator.SetBool("isChasing", false);
            ctx.MonsterAnimator.SetBool("isAttacking", true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            ctx.MonsterAnimator.SetBool("isAttacking", false);
        }

        public override void InitializeSubstate() { }

        public override void CheckSwitchStates()
        {
            ctx.PlayerInSightRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.SightRange, ctx.WhatIsPlayer);
            ctx.PlayerInAttackRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.AttackRange, ctx.WhatIsPlayer);

            if (ctx.PlayerInSightRange && !ctx.PlayerInAttackRange)
            {
                SwitchState(factory.Chasing());
                Debug.Log("AttackingToChasing");
            }
        }


    }
}