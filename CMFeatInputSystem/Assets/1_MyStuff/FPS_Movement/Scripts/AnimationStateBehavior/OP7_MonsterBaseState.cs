using UnityEngine;

public abstract class MonsterBaseState
{
    public abstract void EnterState(OP7_MonsterStateManager monster);

    public abstract void UpdateState(OP7_MonsterStateManager monster);

    public abstract void OnCollisionEnter(OP7_MonsterStateManager monster);
}
