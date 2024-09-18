using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiscountDelirium
{
    public class GameCheckoutState : GameState
	{
		public override void OnEnter()
		{
			Debug.LogWarning("GameState : CHECKOUT");
			Timer.TimesUp += GameOver;
		}

		public override void OnExit()
		{
			Debug.LogWarning("GameState Exit : CHECKOUT");
		}

		public override void OnUpdate()
		{
		}

		public override void OnFixedUpdate()
		{
		}

		public override bool CanEnter(IState currentState)
		{
			if (currentState is GameplayState)
			{
				return m_gameStateMachine.IsCheckingOut;
			}
			
			return false;
		}

		public override bool CanExit()
		{
			return !m_gameStateMachine.IsCheckingOut;
		}

		private void GameOver()
		{
			m_gameStateMachine.m_isGameOver = true;
			Debug.Log("GameOver");
		}

	}
}
