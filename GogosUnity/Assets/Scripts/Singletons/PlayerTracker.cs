using System;
using UnityEngine;

namespace Gogos
{
    public class PlayerTracker : AbstractSingleton<PlayerTracker>
    {
        public static event Action PlayerChanged;

        public static Player Player => Players[s_PlayerIndex];

        public static Player[] Players { get; private set; } = new Player[PlayerCount];

        public const int PlayerCount = 3;

        [SerializeField]
        private ScriptableGogoBucket m_StarterGogos;

        private static int s_PlayerIndex;

        protected override void Awake()
        {
            base.Awake();
            var playerColors = Enum.GetValues(typeof(PlayerColor));
            for (int i = 0; i < PlayerCount; i++)
            {
                var playerName = $"Player{i + 1}";
                var playerColor = (PlayerColor)playerColors.GetValue(i);
                var player = new Player(playerName, playerColor);
                foreach (var scriptableGogo in m_StarterGogos.ScriptableGogos)
                {
                    player.Collection.Add(new IdentifiableGogo(scriptableGogo));
                }
                Players[i] = player;
            }
            s_PlayerIndex = 0;
        }

        public void TransitionToNextPlayer()
        {
            s_PlayerIndex = (s_PlayerIndex + 1) % PlayerCount;
            PlayerChanged?.Invoke();
        }
    }
}
