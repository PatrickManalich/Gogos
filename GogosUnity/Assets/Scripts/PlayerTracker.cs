using System;
using UnityEngine;

namespace Gogos
{
	public class PlayerTracker : MonoBehaviour
	{
		public Player Player => Players[m_PlayerIndex];

		public Player[] Players { get; private set; } = new Player[PlayerCount];

		[SerializeField]
		private ScriptableGogoBucket m_Starters;

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
				player.AddToCollection(m_Starters.ScriptableGogos);
				Players[i] = player;
			}
		}
	}
}