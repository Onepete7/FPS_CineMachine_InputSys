namespace SG
{
    public class SG_Patrolling : SG_State
    {
        public SG_ChasingState sg_chaseState;
        public bool canSeeThePlayer;
        public override SG_State RunCurrentState()
        {
            if (canSeeThePlayer)
            {
                return sg_chaseState;
            }

            else
            {
                return this;

            }
        }
    }
}
