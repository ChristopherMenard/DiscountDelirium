using CartControl;
using System;
using UnityEngine;
using static Manager.AudioManager;

namespace DiscountDelirium
{
    public class EndGameState : GameState
    {
        public static Action OnEndGame;
        public override void OnEnter()
        {
            Debug.LogWarning("GameState : EndGame");
            m_gameStateMachine.m_scoreUI.EnableUI(true);
            m_gameStateMachine.m_scoreUI.ShowScore(PlayerPrefs.GetInt("Score", 0), m_gameStateMachine.m_nbItems, m_gameStateMachine.m_nbOfCartokens, m_gameStateMachine.Score);
            m_gameStateMachine.m_playerSM.IsPaused = true;

            // Désactiver le pause au endGame
            MainInputsHandler mainInput = m_gameStateMachine.m_playerSM.gameObject.GetComponent<MainInputsHandler>();
            mainInput.enabled = false;

			_AudioManager.PlayMusic(EMusic.WaitingRoomMusic);

			OnEndGame.Invoke();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnExit()
        {

        }

        public override bool CanEnter(IState currentState)
        {
            if (currentState is GameplayState || currentState is GameCheckoutState)
            {
                return m_gameStateMachine.m_isGameOver;
            }

			return false;
        }

        public override bool CanExit()
        {
            return false;
        }

    }
}
