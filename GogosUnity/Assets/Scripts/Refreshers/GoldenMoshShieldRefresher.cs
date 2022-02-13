using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class GoldenMoshShieldRefresher : MonoBehaviour
    {
        [SerializeField]
        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        [SerializeField]
        private RangeTierTracker m_RangeTierTracker;

        private static readonly Dictionary<ShieldStrengthTier, RangeTier> RangeTiersByShieldStrengthTier = new Dictionary<ShieldStrengthTier, RangeTier>()
        {
            { ShieldStrengthTier.Broken, RangeTier.Small },
            { ShieldStrengthTier.Weak, RangeTier.Small },
            { ShieldStrengthTier.Medium, RangeTier.Medium },
            { ShieldStrengthTier.Strong, RangeTier.Large },
            { ShieldStrengthTier.Unbreakable, RangeTier.Large },
        };

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
            var rangeTier = RangeTiersByShieldStrengthTier[m_ShieldStrengthTierTracker.Tier];
            m_RangeTierTracker.SetTier((int)rangeTier);
        }
    }
}
