using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class PlayerDetails : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_NameText;

        [SerializeField]
        private TextMeshProUGUI m_GoldenGogosText;

        [SerializeField]
        private GameObject m_SelectedIndicator;

        [SerializeField]
        private Image m_FillImage;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        private const float NormalFontSize = 28;
        private const float SelectedFontSize = 38;
        private static readonly Color NormalFillColor = new Color(0.9f, 0.9f, 0.9f);
        private static readonly Color SelectedFillColor = Color.white;

        private Player m_Player;

        private void OnDestroy()
        {
            if (m_Player != null)
            {
                m_Player.Collection.GogoAdded -= PlayerCollection_OnGogoAdded;
            }
            PlayerTracker.PlayerChanged -= RefreshSelectedElements;
        }

        public void SetDetails(Player player)
        {
            m_Player = player;
            m_NameText.text = m_Player.Name;
            m_NameText.color = m_ScriptableColorPalette.GetColorForPlayerColor(m_Player.PlayerColor);

            PlayerTracker.PlayerChanged += RefreshSelectedElements;
            RefreshSelectedElements();

            m_Player.Collection.GogoAdded += PlayerCollection_OnGogoAdded;
            RefreshGoldenGogosText();
        }

        private void PlayerCollection_OnGogoAdded(object sender, IdentifiableGogoEventArgs e)
        {
            RefreshGoldenGogosText();
        }

        private void RefreshSelectedElements()
        {
            var isSelected = m_Player == PlayerTracker.Player;
            m_NameText.fontSize = isSelected ? SelectedFontSize : NormalFontSize;
            m_SelectedIndicator.SetActive(isSelected);
            m_FillImage.color = isSelected ? SelectedFillColor : NormalFillColor;
        }

        private void RefreshGoldenGogosText()
        {
            var goldenGogosCount = m_Player.Collection.IdentifiableGogos.Count(i => i.ScriptableGogo.GogoClass == GogoClass.Golden);
            m_GoldenGogosText.text = goldenGogosCount.ToString();
        }
    }
}
