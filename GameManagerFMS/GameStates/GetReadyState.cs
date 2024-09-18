using System;
using UnityEngine;
using static Manager.AudioManager;

namespace DiscountDelirium
{
    public class GetReadyState : GameState
    {
        public static Action OnPlayerReady;
        public static Action OnGameStarted;

        public override void OnEnter()
        {
            Debug.LogWarning("GameState : GetReady");
            StartGameTimer.OnStartingTimeEnded += StartGame;
            m_gameStateMachine.m_playerSM.IsPaused = true;
            
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnExit()
        {
            Debug.LogWarning("GameState Exit : GetReady");
        }

        public override bool CanEnter(IState currentState)
        {
            return !m_gameStateMachine.m_isGameStarted;
        }

        public override bool CanExit()
        {
            return m_gameStateMachine.m_isGameStarted;
        }

        private void StartGame()
        {
            OnGameStarted?.Invoke();
			_AudioManager.StartCurrentSceneMusic();
			m_gameStateMachine.m_playerSM.IsPaused = false;
            m_gameStateMachine.m_isGameStarted = true;
        }
    }
}
