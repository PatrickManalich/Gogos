using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class Portrait : MonoBehaviour
    {
        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private GameObject m_LaunchedIndicator;

        [SerializeField]
        private GameObject m_CollectedIndicator;

        public void SetPortrait(Sprite sprite, Situation situation)
        {
            m_Image.sprite = sprite;
            m_Image.color = Color.white;
            m_LaunchedIndicator.SetActive(situation == Situation.Launched);
            m_CollectedIndicator.SetActive(situation == Situation.Collected);
        }
    }
}
