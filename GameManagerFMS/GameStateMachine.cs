using CartControl;
using StatsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DiscountDelirium
{
    public class GameStateMachine : StateMachine<GameState>
    {
        public static GameStateMachine Instance { get; private set; }
        [field:SerializeField] public float GameLength { get; private set; }
		[field: SerializeField] public List<GameObject> ClientsList { get; private set; }

        public int Score { get; private set; } = 0;
        private int m_lastScore;

		public CartStateMachine m_playerSM; 
        [SerializeField] public ScoreUI m_scoreUI;
        [SerializeField] public UpgradeManager m_upgrademanager;


        [field: SerializeField] public bool IsGamePaused { get; set; }
        [field: SerializeField] public bool IsCheckingOut { get; set; }

        [HideInInspector] public bool m_isGameOver;//field
        [HideInInspector] public bool m_isGameStarted;//field
        [HideInInspector] public int m_nbItems = 0;
        [HideInInspector] public int m_nbOfCartokens = 0;

        


        private void Awake()
        {
            if (Instance != null && Instance != this) 
            {
                Destroy(this);
                return;
            }       
            Instance = this;

            m_lastScore = PlayerPrefs.GetInt("Score", 0);
        }

        protected override void Start()
        {
            base.Start();
            foreach (GameState state in m_possibleStates)
            {
                state.OnStart(this);
            }
            m_currentState.OnEnter();
        }

        protected override void Update()
        {
            m_currentState.OnUpdate();
            TryToChangeState();
        }

        protected override void FixedUpdate()
        {
            m_currentState.OnFixedUpdate();
        }

        protected override void CreatePossibleStateList()
        {
            m_possibleStates.Add(new GetReadyState());
            m_possibleStates.Add(new GameplayState());
            m_possibleStates.Add(new PauseState());
            m_possibleStates.Add(new EndGameState());
            m_possibleStates.Add(new GameCheckoutState());
        }

        
        public void GetScoreFromCart(Vector3 data) 
        {         
            m_nbItems += (int)data.x;
            Score += (int)data.y;
            PlayerPrefs.SetInt("Score", m_lastScore + Score);
			m_nbOfCartokens += (int)data.z;

            m_upgrademanager.AddMoney((int)data.z);
		}

        public GameState GetCurrentState()
        {
            return m_currentState;
        }
    }
}
