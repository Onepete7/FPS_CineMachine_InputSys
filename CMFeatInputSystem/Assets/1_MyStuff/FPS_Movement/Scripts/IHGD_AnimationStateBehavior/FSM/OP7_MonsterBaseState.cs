namespace IHGD
{
    public abstract class MonsterBaseState
    {
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchStates();

        public abstract void InitializeSubstate();

        void UpdateStates() { }
        void SwitchState() { }
        void SetSuperState() { }
        void SetSubState() { }


    }

}