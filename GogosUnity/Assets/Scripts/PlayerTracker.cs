using System;
using UnityEngine;

namespace Gogos
{
    public class PlayerTracker : AbstractSingleton<PlayerTracker>
    {
        public static event Action PlayerChanged;

        public static Player Player => Players[m_PlayerIndex];

        public static Player[] Players { get; private set; } = new Player[PlayerCount];

        [SerializeField]
        private ScriptableGogoBucket m_Starters;

        [SerializeField]
        private PhaseTracker m_PhaseTracker;

        private const int PlayerCount = 3;

        private static int m_PlayerIndex;

        protected override void Awake()
        {
            base.Awake();
            var playerColors = Enum.GetValues(typeof(PlayerColor));
            for (int i = 0; i < PlayerCount; i++)
            {
                var playerName = $"Player{i + 1}";
                var playerColor = (PlayerColor)playerColors.GetValue(i);
                var player = new Player(playerName, playerColor);
                player.Collection.Add(m_Starters.ScriptableGogos);
                Players[i] = player;
            }
        }

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Transitioning)
            {
                m_PlayerIndex = (m_PlayerIndex + 1) % PlayerCount;
                PlayerChanged?.Invoke();
            }
        }
    }
}
