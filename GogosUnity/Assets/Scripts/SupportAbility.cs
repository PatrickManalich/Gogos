using UnityEngine;

namespace Gogos
{
	public class SupportAbility : MonoBehaviour
	{
        [SerializeField]
        private TierVariant m_TierVariant;

        [Range(-3, 3)]
        [SerializeField]
        private int m_TierModifier;

        public void ProvideSupport(AbstractTierTracker tierTracker)
        {
            if (tierTracker.TierVariant == m_TierVariant)
            {
                tierTracker.Modify(m_TierModifier);
            }
        }

        public void RemoveSupport(AbstractTierTracker tierTracker)
        {
            if (tierTracker.TierVariant == m_TierVariant)
            {
                tierTracker.Modify(-m_TierModifier);
            }
        }
    }
}