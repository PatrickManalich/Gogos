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

        private const float NormalFontSize = 28;
        private const float SelectedFontSize = 38;

        private PlayerTracker m_PlayerTracker;
        private Player m_Player;

        private void Awake()
        {
            m_PlayerTracker = FindObjectOfType<PlayerTracker>();
        }

        private void Start()
        {
            m_PlayerTracker.PlayerChanged += RefreshSelectedElements;
        }

        private void OnDestroy()
        {
            m_PlayerTracker.PlayerChanged -= RefreshSelectedElements;
        }

        public void SetDetails(Player player)
        {
            m_Player = player;
            m_NameText.text = m_Player.Name;
            m_NameText.color = m_ColorsByPlayerColor[m_Player.PlayerColor];
            RefreshSelectedElements();
        }

        private void RefreshSelectedElements()
        {
            var isSelected = m_Player == m_PlayerTracker.Player;
            m_NameText.fontSize = isSelected ? SelectedFontSize : NormalFontSize;
            m_SelectedIndicator.SetActive(isSelected);
        }
    }
}
