using UnityEngine;

namespace SG
{
    public class SG_AttackingState : SG_State
    {
        public override SG_State RunCurrentState()
        {
            Debug.Log("I have attacked");
            return this;
        }
    }
}
