using UnityEngine;

namespace DiscountDelirium
{
    public class GameplayState : GameState
    {
        public override void OnEnter()
        {
            Debug.LogWarning("GameState : GAMEPLAY");
            Timer.TimesUp += GameOver;
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnExit()
        {
            Debug.LogWarning("GameState Exit : GAMEPLAY");
        }

        public override bool CanEnter(IState currentState)
        {
            if (currentState is GetReadyState)
            {
                return m_gameStateMachine.m_isGameStarted;
            }
            if (currentState is PauseState)
            {
                return !m_gameStateMachine.IsGamePaused;
            }
            return !m_gameStateMachine.m_isGameOver;
        }

        public override bool CanExit()
        {
            return m_gameStateMachine.m_playerSM.IsPaused || m_gameStateMachine.m_isGameOver;
        }

        private void GameOver() 
        {
            m_gameStateMachine.m_isGameOver = true;
            Debug.Log("GameOver");
        }
    }
}
