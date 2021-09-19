using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    [System.Serializable]
    public class ButtonSpriteSet
    {
        [SerializeField]
        private Sprite m_ImageSprite;
        public Sprite ImageSprite => m_ImageSprite;

        [SerializeField]
        private SpriteState m_SpriteState;
        public SpriteState SpriteState => m_SpriteState;
    }
}
