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
            ctx.MonsterNavMeshAgent.SetDestination(ctx.MonsterTransform.position);
            ctx.MonsterTransform.LookAt(ctx.playerTransform);
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

            if (ctx.PlayerInSightRange && !ctx.PlayerInAttackRange && !IsAnimationPlaying(ctx.MonsterAnimator, ctx.MONSTER_ATTACKING))
            {
                Debug.Log("AttackingToChasing");
                SwitchState(factory.Chasing());
            }

            if (!ctx.PlayerInSightRange && !ctx.PlayerInAttackRange && !IsAnimationPlaying(ctx.MonsterAnimator, ctx.MONSTER_ATTACKING))
            {
                Debug.Log("AttackingToPatrolling");
                SwitchState(factory.Patrolling());
            }
        }

        void AttackPlayer()
        {
            ChangeAnimationState(ctx.MONSTER_ATTACKING);
        }




        void ChangeAnimationState(string newAnimationState)
        {
            if (newAnimationState == ctx.CurrentAnimationState)
            {
                return;
            }

            ctx.MonsterAnimator.Play(newAnimationState);
            ctx.CurrentAnimationState = newAnimationState;
        }

        bool IsAnimationPlaying(Animator animator, string animationStateName)
        {
            if (ctx.MonsterAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName) &&
                ctx.MonsterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
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