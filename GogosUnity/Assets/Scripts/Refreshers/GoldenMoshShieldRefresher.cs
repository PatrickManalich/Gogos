using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class GoldenMoshShieldRefresher : MonoBehaviour
    {
        [SerializeField]
        private ShieldDurabilityTierTracker m_ShieldDurabilityTierTracker;

        [SerializeField]
        private RangeTierTracker m_RangeTierTracker;

        private static readonly Dictionary<ShieldDurabilityTier, RangeTier> RangeTiersByShieldDurabilityTier = new Dictionary<ShieldDurabilityTier, RangeTier>()
        {
            { ShieldDurabilityTier.Broken, RangeTier.Disabled },
            { ShieldDurabilityTier.Weak, RangeTier.Small },
            { ShieldDurabilityTier.Medium, RangeTier.Medium },
            { ShieldDurabilityTier.Strong, RangeTier.Large },
            { ShieldDurabilityTier.Unbreakable, RangeTier.Large },
        };

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
            var rangeTier = RangeTiersByShieldDurabilityTier[m_ShieldDurabilityTierTracker.Tier];
            m_RangeTierTracker.SetTier((int)rangeTier);
        }
    }
}
