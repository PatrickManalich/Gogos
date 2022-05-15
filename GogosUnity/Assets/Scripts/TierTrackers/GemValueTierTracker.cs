using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public enum GemValueTier { Low, Medium, High }

    public class GemValueTierTracker : AbstractTierTracker<GemValueTier>
    {
        public override TierVariant TierVariant => TierVariant.GemValue;

        public int GemValue => GemValuesByTier[Tier];

        public GogoClass GogoClass => m_GogoClass;

        [SerializeField]
        private GogoClass m_GogoClass;

        private static readonly Dictionary<GemValueTier, int> GemValuesByTier = new Dictionary<GemValueTier, int>()
        {
            { GemValueTier.Low, 5 },
            { GemValueTier.Medium, 10 },
            { GemValueTier.High, 20 },
        };
    }
}
