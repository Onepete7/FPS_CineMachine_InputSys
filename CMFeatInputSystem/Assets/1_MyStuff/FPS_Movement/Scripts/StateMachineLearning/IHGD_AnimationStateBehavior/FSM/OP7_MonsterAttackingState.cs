using UnityEngine;
using System.Collections;

namespace IHGD
{


    public class OP7_MonsterAttackingState : MonsterBaseState
    {
        public OP7_MonsterAttackingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }

        public override void EnterState()
        {
            Debug.Log("AttackingStateEntered");
        }

        public override void UpdateState()
        {
            AttackPlayer();
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Debug.Log("ExitingAttackingState");
        }

        public override void InitializeSubstate() { }

        public override void CheckSwitchStates()
        {
            ctx.PlayerInSightRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.SightRange, ctx.WhatIsPlayer);
            ctx.PlayerInAttackRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.AttackRange, ctx.WhatIsPlayer);

            if (!ctx.PlayerInAttackRange)
            {
                Debug.Log("AttackingToChasing");
                SwitchState(factory.Chasing());
            }
            else
            {
                SwitchState(factory.Attacking());
            }
        }

        private void AttackPlayer()
        {
            //Should find a way for the animation to play completely and cache the animatorstateinfo
            ctx.MonsterAnimator.CrossFade("Attacking", 0f, 0, 2.667f);
        }
    }
}