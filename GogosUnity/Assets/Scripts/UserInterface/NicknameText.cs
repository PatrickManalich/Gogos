using TMPro;
using UnityEngine;

namespace Gogos
{
    public class NicknameText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

        public void SetText(string text, RarityTier rarityTier)
        {
            m_Text.text = text;
            m_Text.color = m_ScriptableColorPalette.GetColorForRarityTier(rarityTier);
        }
    }
}
