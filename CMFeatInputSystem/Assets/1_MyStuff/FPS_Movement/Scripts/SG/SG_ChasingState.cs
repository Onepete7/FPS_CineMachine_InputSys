using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SG_ChasingState : SG_State
    {
        public SG_AttackingState sG_attackingState;
        public bool isInAttackRange;
        public override SG_State RunCurrentState()
        {
            if (isInAttackRange)
            {
                return sG_attackingState;
            }
            else
            {
                return this;

            }
        }
    }
}
