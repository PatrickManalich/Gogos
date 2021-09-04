using RotaryHeart.Lib.SerializableDictionaryPro;
using TMPro;
using UnityEngine;

namespace Gogos
{
	public class PlayerDetails : MonoBehaviour
	{
        [System.Serializable]
        private class ColorsByPlayerColor : SerializableDictionary<PlayerColor, Color> { }

        [SerializeField]
        private TextMeshProUGUI m_NameText;

        [SerializeField]
        private ColorsByPlayerColor m_ColorsByPlayerColor;

        [SerializeField]
        private GameObject m_SelectedIndicator;

		public void SetDetails(Player player)
        {
            m_NameText.text = player.Name;
            m_NameText.color = m_ColorsByPlayerColor[player.PlayerColor];
            m_SelectedIndicator.SetActive(player == GameManager.PlayerManager.CurrentPlayer);
        }
	}
}