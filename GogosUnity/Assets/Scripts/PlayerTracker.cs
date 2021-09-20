using System;
using UnityEngine;

namespace Gogos
{
    public class PlayerTracker : MonoBehaviour
    {
        public event Action PlayerChanged;

        public Player Player => Players[m_PlayerIndex];

        public Player[] Players { get; private set; } = new Player[PlayerCount];

        [SerializeField]
        private ScriptableGogoBucket m_Starters;

        [SerializeField]
        private PhaseTracker m_PhaseTracker;

        private const int PlayerCount = 3;

        private int m_PlayerIndex;

        private void Awake()
        {
            var playerColors = Enum.GetValues(typeof(PlayerColor));
            for (int i = 0; i < PlayerCount; i++)
            {
                var playerName = $"Player{i + 1}";
                var playerColor = (PlayerColor)playerColors.GetValue(i);
                var player = new Player(playerName, playerColor);
                player.Collection.AddRange(m_Starters.ScriptableGogos);
                Players[i] = player;
            }
        }

        private void Start()
        {
            m_PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            m_PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (m_PhaseTracker.Phase == Phase.Selecting)
            {
                m_PlayerIndex = (m_PlayerIndex + 1) % PlayerCount;
                PlayerChanged?.Invoke();
            }
        }
    }
}
