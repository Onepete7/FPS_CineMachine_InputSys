


namespace IHGD
{


    using UnityEngine.AI;
    using UnityEngine;

    public class OP7_MonsterChasingState : MonsterBaseState
    {
        public OP7_MonsterChasingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }
        public override void EnterState()
        {
            Debug.Log("IM IN THE CHASING STATE BITCH");
        }

        public override void UpdateState() { }

        public override void ExitState() { }

        public override void CheckSwitchStates() { }

        public override void InitializeSubstate() { }
    }
}