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
        private TextMeshProUGUI m_PointsText;

        [SerializeField]
        private ColorsByPlayerColor m_ColorsByPlayerColor;

        [SerializeField]
        private GameObject m_SelectedIndicator;

        private const float NormalFontSize = 28;
        private const float SelectedFontSize = 38;

        private Player m_Player;

        private void OnDestroy()
        {
            if (m_Player != null)
            {
                m_Player.PointsAdded -= RefreshPointsText;
            }
            PlayerTracker.PlayerChanged -= RefreshSelectedElements;
        }

        public void SetDetails(Player player)
        {
            m_Player = player;
            m_NameText.text = m_Player.Name;
            m_NameText.color = m_ColorsByPlayerColor[m_Player.PlayerColor];

            RefreshSelectedElements();
            PlayerTracker.PlayerChanged += RefreshSelectedElements;

            RefreshPointsText();
            m_Player.PointsAdded += RefreshPointsText;
        }

        private void RefreshSelectedElements()
        {
            var isSelected = m_Player == PlayerTracker.Player;
            m_NameText.fontSize = isSelected ? SelectedFontSize : NormalFontSize;
            m_SelectedIndicator.SetActive(isSelected);
        }

        private void RefreshPointsText()
        {
            m_PointsText.text = m_Player.Points.ToString();
        }
    }
}
