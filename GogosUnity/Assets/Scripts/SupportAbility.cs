using System;
using UnityEngine;

namespace Gogos
{
    [Flags]
    public enum SupportableGroups { AllyGogos = 1, EnemyGogos = 2, UnclaimedGogos = 4 } // Index 0 is for "Nothing" in the editor

    public class SupportAbility : MonoBehaviour
    {
        [SerializeField]
        private AbstractGogo m_Gogo;

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

        public bool CanSupport(Player player, AbstractTierTracker tierTracker)
        {
            var canSupport = false;
            if (m_SupportableGroups.HasFlag(SupportableGroups.AllyGogos) && player == m_Gogo.Player)
            {
                canSupport = true;
            }
            if (m_SupportableGroups.HasFlag(SupportableGroups.EnemyGogos) && player != null && player != m_Gogo.Player)
            {
                canSupport = true;
            }
            if (m_SupportableGroups.HasFlag(SupportableGroups.UnclaimedGogos) && player == null)
            {
                canSupport = true;
            }
            canSupport = canSupport && tierTracker.TierVariant == m_TierVariant;
            return canSupport;
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
