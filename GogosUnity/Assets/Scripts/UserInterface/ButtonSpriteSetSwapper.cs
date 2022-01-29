using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class ButtonSpriteSetSwapper : MonoBehaviour
    {
        [System.Serializable]
        private class ButtonSpriteSetsByPlayerColor : SerializableDictionary<PlayerColor, ButtonSpriteSet> { }

        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private Button m_Button;

        [SerializeField]
        private ButtonSpriteSetsByPlayerColor m_ButtonSpriteSetsByPlayerColor;

        private void Start()
        {
            PlayerTracker.PlayerChanged += Refresh;

            Refresh();
        }

        private void OnDestroy()
        {
            PlayerTracker.PlayerChanged -= Refresh;
        }

        private void Refresh()
        {
            var buttonSpriteSet = m_ButtonSpriteSetsByPlayerColor[PlayerTracker.Player.PlayerColor];
            m_Image.sprite = buttonSpriteSet.ImageSprite;
            m_Button.spriteState = buttonSpriteSet.SpriteState;
        }
    }
}
