using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class TierSlot : MonoBehaviour
    {
        [SerializeField]
        private Image m_Image;

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
    }
}
