using UnityEngine;

namespace Gogos
{
    public class ShieldVisualRefresher : MonoBehaviour
    {
        [SerializeField]
        private ShieldDurabilityTierTracker m_ShieldDurabilityTierTracker;

        [SerializeField]
        private GameObject m_ShieldVisual;

        private void OnEnable()
        {
            m_ShieldDurabilityTierTracker.TierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_ShieldDurabilityTierTracker.TierChanged -= Refresh;
        }

        public void Refresh()
        {
            m_ShieldVisual.gameObject.SetActive(!m_ShieldDurabilityTierTracker.IsShieldBroken);
        }
    }
}
