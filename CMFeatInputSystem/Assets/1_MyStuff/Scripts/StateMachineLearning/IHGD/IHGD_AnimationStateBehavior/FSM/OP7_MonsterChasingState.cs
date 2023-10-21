namespace IHGD
{
    using UnityEngine;

    public class OP7_MonsterChasingState : MonsterBaseState
    {
        public OP7_MonsterChasingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }

        public override void EnterState()
        {
            ChangeAnimationState(ctx.MONSTER_CHASING);
            Debug.Log("ChasingStateEntered");
        }

        public override void UpdateState()
        {
            ChasePlayer();
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            ctx.MonsterNavMeshAgent.SetDestination(ctx.MonsterTransform.position);
            Debug.Log("ExitingChasingState");
        }

        public override void InitializeSubstate() { }
        public override void CheckSwitchStates()
        {
            ctx.PlayerInSightRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.SightRange, ctx.WhatIsPlayer);
            ctx.PlayerInAttackRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.AttackRange, ctx.WhatIsPlayer);

            if (!ctx.PlayerInSightRange && !ctx.PlayerInAttackRange)
            {
                Debug.Log("ChasingToPatrolling");
                SwitchState(factory.Patrolling());
            }

            else if (ctx.PlayerInSightRange && ctx.PlayerInAttackRange)
            {
                Debug.Log("ChasingToAttacking");
                SwitchState(factory.Attacking());
            }
        }



        private void ChasePlayer()
        {
            ctx.MonsterNavMeshAgent.SetDestination(ctx.PlayerTransform.position);
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