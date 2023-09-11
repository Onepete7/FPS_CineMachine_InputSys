using UnityEngine.AI;
using UnityEngine;
using System;

public class OP7_MonsterAttackingState : MonsterBaseState
{
    Animator monsterAnimator;
    NavMeshAgent agent;
    public float timeBetweenAttacks = 2.0f;
    bool alreadyAttacked;

    int numberOfAttacks;

    public override void EnterState(OP7_MonsterStateManager monster)
    {
        Debug.Log("GET FICKITIFOCKED");
        agent.SetDestination(monster.transform.position);
        monsterAnimator.SetBool("isAttacking", true);
        numberOfAttacks = 0;
    }

    public override void UpdateState(OP7_MonsterStateManager monster)
    {
        void MonsterAttack()
        {
            Debug.Log("Number of times you have been deaded : " + numberOfAttacks);
            numberOfAttacks++;
        }

        if (!alreadyAttacked)
        {
            MonsterAttack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        void ResetAttack()
        {
            alreadyAttacked = false;
        }
    }

    private void Invoke(string v, float timeBetweenAttacks)
    {
        throw new NotImplementedException();
    }

}
