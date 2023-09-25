using UnityEngine;

namespace SG
{
    public class SG_StateManager : MonoBehaviour
    {
        public SG_State currentState;
        private void Update()
        {
            RunStateMachine();
        }

        private void RunStateMachine()
        {
            SG_State nextState = currentState?.RunCurrentState();

            if (nextState != null)
            {
                SwitchToTheNextState(nextState);
            }
        }

        private void SwitchToTheNextState(SG_State nextState)
        {
            currentState = nextState;
        }
    }
}