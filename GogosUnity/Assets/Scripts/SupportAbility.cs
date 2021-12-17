using System;
using UnityEngine;

namespace Gogos
{
    [Flags]
    public enum SupportableGroups { None = 0, AllyGogos = 1, EnemyGogos = 2, UnclaimedGogos = 4 }

    public class SupportAbility : MonoBehaviour
    {
        [SerializeField]
        private SupportableGroups m_SupportableGroups;

        [SerializeField]
        private TierVariant m_TierVariant;

        [Range(MinSupport, MaxSupport)]
        [SerializeField]
        private int m_TierModifier;

        public const int MinSupport = -3;
        public const int MaxSupport = 3;

        public void SetAbility(SupportableGroups supportableGroups, TierVariant tierVariant, int tierModifier)
        {
            m_SupportableGroups = supportableGroups;
            m_TierVariant = tierVariant;
            m_TierModifier = Mathf.Min(Mathf.Max(tierModifier, MinSupport), MaxSupport);
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
