using UnityEngine;

namespace IHGD
{



    public class OP7_MonsterPatrollingState : MonsterBaseState
    {
        public OP7_MonsterPatrollingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }
        public override void EnterState()
        {
            ChangeAnimationState(ctx.MONSTER_PATROLLING);
            Debug.Log("PatrollingStateEntered");
        }

        public override void UpdateState()
        {
            Patrolling();
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Debug.Log("ExitingPatrollingState");
        }

        public override void CheckSwitchStates()
        {
            ctx.PlayerInSightRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.SightRange, ctx.WhatIsPlayer);
            ctx.PlayerInAttackRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.AttackRange, ctx.WhatIsPlayer);
            //if player is seen, get chased
            if (ctx.PlayerInSightRange && !ctx.PlayerInAttackRange)
            {
                Debug.Log("PatrollingToChasing");
                SwitchState(factory.Chasing());
            }
        }

        public override void InitializeSubstate() { }




        private void Patrolling()
        {
            if (!ctx.WalkPointSet) SearchWalkPoint();

            if (ctx.WalkPointSet)
                ctx.MonsterNavMeshAgent.SetDestination(ctx.WalkPoint);

            Vector3 distanceToWalkPoint = ctx.MonsterTransform.position - ctx.WalkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                ctx.WalkPointSet = false;
        }
        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-ctx.WalkPointRange, ctx.WalkPointRange);
            float randomX = Random.Range(-ctx.WalkPointRange, ctx.WalkPointRange);

            ctx.WalkPoint = new Vector3(ctx.MonsterTransform.position.x + randomX, ctx.MonsterTransform.position.y, ctx.MonsterTransform.position.z + randomZ);

            if (Physics.Raycast(ctx.WalkPoint, -ctx.MonsterTransform.up, 2f, ctx.WhatIsGround))
                ctx.WalkPointSet = true;
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