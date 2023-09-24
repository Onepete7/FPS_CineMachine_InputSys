using UnityEngine;

namespace IHGD
{



    public class OP7_MonsterPatrollingState : MonsterBaseState
    {
        public OP7_MonsterPatrollingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }
        public override void EnterState()
        {
            Debug.Log("ENTERING PATROLLING");
            ctx.MonsterAnimator.SetBool("isChasing", false);
            ctx.MonsterAnimator.SetBool("isPatrolling", true);
        }

        public override void UpdateState()
        {
            Patrolling();
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            ctx.MonsterAnimator.SetBool("isPatrolling", false);
        }

        public override void CheckSwitchStates()
        {
            ctx.PlayerInSightRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.SightRange, ctx.WhatIsPlayer);
            ctx.PlayerInAttackRange = Physics.CheckSphere(ctx.MonsterTransform.position, ctx.AttackRange, ctx.WhatIsPlayer);
            //if player is seen, get chased
            if (ctx.PlayerInSightRange && !ctx.PlayerInAttackRange)
            {
                SwitchState(factory.Chasing());
                Debug.Log("PatrollingToChasing");
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

    }
}