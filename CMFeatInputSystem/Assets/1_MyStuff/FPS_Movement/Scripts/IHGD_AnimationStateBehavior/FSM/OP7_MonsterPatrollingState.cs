using UnityEngine.AI;
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
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState() { }

        public override void CheckSwitchStates()
        {
            //if player is seen, get chased
            if (ctx.isPlayerBeingChased)
            {
                SwitchState(factory.Chasing());
                Debug.Log("HEY YOU LA BAS!");
            }
        }

        public override void InitializeSubstate() { }

    }
}