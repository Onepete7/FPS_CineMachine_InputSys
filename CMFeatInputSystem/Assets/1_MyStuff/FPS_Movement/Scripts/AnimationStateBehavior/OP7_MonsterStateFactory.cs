public class MonsterStateFactory
{
    OP7_MonsterStateManager context;

    public MonsterStateFactory(OP7_MonsterStateManager currentContext)
    {
        context = currentContext;
    }

    public MonsterBaseState Patrolling()
    {
        return new OP7_MonsterPatrollingState();
    }

    public MonsterBaseState Chasing()
    {
        return new OP7_MonsterChasingState();
    }

    public MonsterBaseState Attacking()
    {
        return new OP7_MonsterAttackingState();
    }
}