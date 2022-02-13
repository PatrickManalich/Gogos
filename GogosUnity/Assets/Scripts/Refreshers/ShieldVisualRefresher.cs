using UnityEngine;

namespace Gogos
{
    public class ShieldVisualRefresher : MonoBehaviour
    {
        [SerializeField]
        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        [SerializeField]
        private GameObject m_ShieldVisual;

        private void OnEnable()
        {
            m_ShieldStrengthTierTracker.TierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_ShieldStrengthTierTracker.TierChanged -= Refresh;
        }

        public void Refresh()
        {
            m_ShieldVisual.gameObject.SetActive(!m_ShieldStrengthTierTracker.IsShieldBroken);
        }
    }
}
