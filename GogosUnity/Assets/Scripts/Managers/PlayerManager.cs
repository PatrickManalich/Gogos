using System;
using UnityEngine;

namespace Gogos
{
	public class PlayerManager : MonoBehaviour
	{
        [SerializeField]
        private ScriptableGogoBucket m_Starters;

		private const int PlayerCount = 3;

		private Player[] m_Players = new Player[PlayerCount];

        private void Awake()
		{
			var playerColors = Enum.GetValues(typeof(PlayerColor));
			for (int i = 0; i < PlayerCount; i++)
            {
				var playerName = $"Player{i + 1}";
				var playerColor = (PlayerColor)playerColors.GetValue(i);
				var player = new Player(playerName, playerColor);
				player.AddToCollection(m_Starters.ScriptableGogos);
				m_Players[i] = player;
            }
		}
	}
}