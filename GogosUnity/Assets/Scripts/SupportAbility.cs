using UnityEngine;

namespace Gogos
{
    public class SupportAbility : MonoBehaviour
    {
        [SerializeField]
        private TierVariant m_TierVariant;

        [Range(MinSupport, MaxSupport)]
        [SerializeField]
        private int m_TierModifier;

        public const int MinSupport = -3;
        public const int MaxSupport = 3;

        public void SetAbility(TierVariant tierVariant, int tierModifier)
        {
            m_TierVariant = tierVariant;
            var clampedTierModifier = Mathf.Min(Mathf.Max(tierModifier, MinSupport), MaxSupport);
            m_TierModifier = clampedTierModifier;
        }

        public bool CanSupport(AbstractTierTracker tierTracker)
        {
            return tierTracker.TierVariant == m_TierVariant;
        }

        public void ProvideSupport(AbstractTierTracker tierTracker)
        {
            tierTracker.ModifyTier(m_TierModifier);
        }

        public void RemoveSupport(AbstractTierTracker tierTracker)
        {
            tierTracker.ModifyTier(-m_TierModifier);
        }
    }
}
