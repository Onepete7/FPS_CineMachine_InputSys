using UnityEngine.AI;
using UnityEngine;

public class OP7_MonsterChasingState : MonsterBaseState
{
    Animator monsterAnimator;
    NavMeshAgent agent;
    public Transform player;

    public override void EnterState(OP7_MonsterStateManager monster)
    {

        Debug.Log("I'm going to eat your ass pretty boy");

        player = GameObject.Find("OP7_Player").transform;
        monsterAnimator.SetBool("isRunning", true);
    }

    public override void UpdateState(OP7_MonsterStateManager monster)
    {
        void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        ChasePlayer();
    }

    public override void OnCollisionEnter(OP7_MonsterStateManager monster)
    {

    }
}
