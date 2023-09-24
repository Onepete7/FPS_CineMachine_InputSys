namespace IHGD
{
    public abstract class MonsterBaseState
    {
        protected OP7_MonsterStateManager ctx;
        protected MonsterStateFactory factory;

        public MonsterBaseState(OP7_MonsterStateManager monsterCurrentContext, MonsterStateFactory monsterStateFactory)
        {
            ctx = monsterCurrentContext;
            factory = monsterStateFactory;
        }
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void InitializeSubstate();

        public abstract void CheckSwitchStates();

        void UpdateStates() { }
        protected void SwitchState(MonsterBaseState newMonsterState)
        {
            //current state exits state
            ExitState();

            //new state enters state
            newMonsterState.EnterState();

            // switch current state of context
            ctx.MonsterCurrentState = newMonsterState;
        }
        protected void SetSuperState() { }
        protected void SetSubState() { }


    }

}