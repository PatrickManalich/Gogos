using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class Portrait : MonoBehaviour
    {
        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private GameObject m_InRingIndicator;

        [SerializeField]
        private GameObject m_OffRingIndicator;

        public void SetPortrait(Sprite sprite, Situation situation)
        {
            m_Image.sprite = sprite;
            m_Image.color = Color.white;
            m_InRingIndicator.SetActive(situation == Situation.InRing);
            m_OffRingIndicator.SetActive(situation == Situation.OffRing);
        }
    }
}
