﻿using System.Collections.Generic;

namespace Gogos
{
    public enum RangeTier { Small, Medium, Large }

    public class RangeTierTracker : AbstractTierTracker<RangeTier>
    {
        public override TierVariant TierVariant => TierVariant.Range;
        public float Range => RangeByTier[Tier];

        private static readonly Dictionary<RangeTier, float> RangeByTier = new Dictionary<RangeTier, float>()
        {
            { RangeTier.Small, 10 },
            { RangeTier.Medium, 15 },
            { RangeTier.Large, 20 },
        };
    }
}