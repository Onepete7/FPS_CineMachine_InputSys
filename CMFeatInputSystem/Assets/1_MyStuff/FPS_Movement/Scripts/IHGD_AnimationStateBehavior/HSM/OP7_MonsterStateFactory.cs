

namespace IHGD
{
    public class MonsterStateFactory
    {
        OP7_MonsterStateManager context;

        //THIS IS A CONSTRUCTOR
        public MonsterStateFactory(OP7_MonsterStateManager currentContext)
        {
            context = currentContext;
        }

        public MonsterBaseState Patrolling()
        {
            return new OP7_MonsterPatrollingState(context, this);
        }

        public MonsterBaseState Chasing()
        {
            return new OP7_MonsterChasingState(context, this);
        }

        public MonsterBaseState Attacking()
        {
            return new OP7_MonsterAttackingState(context, this);
        }
    }
}