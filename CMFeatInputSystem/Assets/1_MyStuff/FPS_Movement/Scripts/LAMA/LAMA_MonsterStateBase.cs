using FSM;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace LAMA
{
    public abstract class MonsterStateBase : State<EnumStates, EnumStateEvent>
    {
        protected readonly LAMA_EnemyClass Monster;
        protected readonly NavMeshAgent MonsterNavMeshAgent;
        protected readonly Animator MonsterAnimator;
        protected bool RequestedExit;
        protected float ExitTime;

        protected readonly Action<State<EnumStates, EnumStateEvent>> onEnter;
        protected readonly Action<State<EnumStates, EnumStateEvent>> onLogic;
        protected readonly Action<State<EnumStates, EnumStateEvent>> onExit;
        protected readonly Func<State<EnumStates, EnumStateEvent>, bool> canExit;

        public MonsterStateBase(bool needsExitTime,
            LAMA_EnemyClass Monster,
            float ExitTime = 0.1f,
            Action<State<EnumStates, EnumStateEvent>> onEnter = null,
            Action<State<EnumStates, EnumStateEvent>> onLogic = null,
            Action<State<EnumStates, EnumStateEvent>> onExit = null,
            Func<State<EnumStates, EnumStateEvent>, bool> canExit = null)
        {
            this.Monster = Monster;
            this.Monster = Monster;
            this.Monster = Monster;
            this.Monster = Monster;
            this.Monster = Monster;
            this.Monster = Monster;
            this.Monster = Monster;
            MonsterNavMeshAgent = Monster.GetComponent<NavMeshAgent>();
            MonsterAnimator = Monster.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            RequestedExit = false;
            onEnter?.Invoke(obj: this);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (RequestedExit && timer.Elapsed >= ExitTime)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExitRequest()
        {
            if (!needsExitTime || canExit != null && canExit(this))
            {
                fsm.StateCanExit();
            }
            else
            {
                RequestedExit = true;
            }
        }
    }
}