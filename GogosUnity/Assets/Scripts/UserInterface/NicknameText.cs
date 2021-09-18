using RotaryHeart.Lib.SerializableDictionaryPro;
using TMPro;
using UnityEngine;

namespace Gogos
{
    public class NicknameText : MonoBehaviour
    {
        [System.Serializable]
        private class ColorsByTier : SerializableDictionary<RarityTier, Color> { }

        [SerializeField]
        private TextMeshProUGUI m_Text;

        [SerializeField]
        private ColorsByTier m_ColorsByTier;

        public void SetNickname(string text, RarityTier rarityTier)
        {
            m_Text.text = text;
            m_Text.color = m_ColorsByTier[rarityTier];
        }
    }
}
