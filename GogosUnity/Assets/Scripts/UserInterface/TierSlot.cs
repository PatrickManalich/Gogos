using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class TierSlot : MonoBehaviour
    {
        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private Image m_PlusIcon;

        [SerializeField]
        private Image m_MinusIcon;

        [SerializeField]
        private Color m_FilledColor;

        [SerializeField]
        private Color m_EmptyColor;

        public void Fill()
        {
            m_Image.color = m_FilledColor;
        }

        public void Empty()
        {
            m_Image.color = m_EmptyColor;
        }

        public void ShowPlusIcon()
        {
            m_PlusIcon.gameObject.SetActive(true);
        }

        public void ShowMinusIcon()
        {
            m_MinusIcon.gameObject.SetActive(true);
        }

        public void HideIcons()
        {
            m_PlusIcon.gameObject.SetActive(false);
            m_MinusIcon.gameObject.SetActive(false);
        }
    }
}
