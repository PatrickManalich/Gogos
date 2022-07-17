using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "Gogos/Scriptable Color Palette")]
    public class ScriptableColorPalette : ScriptableObject
    {
        [System.Serializable]
        private class ColorsByPlayerColor : SerializableDictionary<PlayerColor, Color> { }

        [System.Serializable]
        private class ColorsByRarityTier : SerializableDictionary<RarityTier, Color> { }

        [SerializeField]
        private ColorsByPlayerColor m_ColorsByPlayerColor;

        [SerializeField]
        private ColorsByRarityTier m_ColorsByRarityTier;

        [SerializeField]
        private Color m_Grey;
        public Color Grey => m_Grey;

        public Color GetColorForPlayerColor(PlayerColor playerColor)
        {
            return m_ColorsByPlayerColor[playerColor];
        }

        public Color GetColorForRarityTier(RarityTier rarityTier)
        {
            return m_ColorsByRarityTier[rarityTier];
        }
    }
}
