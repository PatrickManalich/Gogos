using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "Gogos/Scriptable Color Palette")]
    public class ScriptableColorPalette : ScriptableObject
    {
        [System.Serializable]
        private class ColorsByPlayerColor : SerializableDictionary<PlayerColor, Color> { }

        [SerializeField]
        private ColorsByPlayerColor m_ColorsByPlayerColor;

        [SerializeField]
        private Color m_Grey;
        public Color Grey => m_Grey;

        public Color GetColorForPlayerColor(PlayerColor playerColor)
        {
            return m_ColorsByPlayerColor[playerColor];
        }
    }
}
