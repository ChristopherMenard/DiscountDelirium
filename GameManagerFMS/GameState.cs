using CartControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiscountDelirium
{
    public class GameState : IState
    {
        protected GameStateMachine m_gameStateMachine;

        public virtual void OnStart(GameStateMachine gameStateMachine)
        {
            m_gameStateMachine = gameStateMachine;
        }

        public virtual void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }


        public virtual void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CanEnter(IState currentState)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CanExit()
        {
            throw new System.NotImplementedException();
        }
    }
}
