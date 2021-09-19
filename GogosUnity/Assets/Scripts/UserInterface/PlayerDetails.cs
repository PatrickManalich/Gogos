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

        private PlayerTracker m_PlayerTracker;
        private Player m_Player;

        private void Awake()
        {
            m_PlayerTracker = FindObjectOfType<PlayerTracker>();
        }

        private void Start()
        {
            m_PlayerTracker.PlayerChanged += PlayerTracker_OnPlayerChanged;
        }

        private void OnDestroy()
        {
            m_PlayerTracker.PlayerChanged -= PlayerTracker_OnPlayerChanged;
        }

        public void SetDetails(Player player)
        {
            m_Player = player;
            m_NameText.text = m_Player.Name;
            m_NameText.color = m_ColorsByPlayerColor[m_Player.PlayerColor];
            m_SelectedIndicator.SetActive(m_Player == m_PlayerTracker.Player);
        }

        private void PlayerTracker_OnPlayerChanged()
        {
            m_SelectedIndicator.SetActive(m_Player == m_PlayerTracker.Player);
        }
    }
}
