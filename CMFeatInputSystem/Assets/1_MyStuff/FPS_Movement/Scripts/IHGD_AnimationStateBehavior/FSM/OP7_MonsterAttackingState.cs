




using UnityEngine.AI;
using UnityEngine;
using System;

namespace IHGD
{


    public class OP7_MonsterAttackingState : MonsterBaseState
    {
        public OP7_MonsterAttackingState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        : base(monsterCurrentContext, monsterStateFactory) { }

        public override void EnterState() { }

        public override void UpdateState() { }

        public override void ExitState() { }

        public override void CheckSwitchStates() { }

        public override void InitializeSubstate() { }
    }
}