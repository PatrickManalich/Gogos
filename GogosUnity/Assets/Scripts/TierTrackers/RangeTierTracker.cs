using System.Collections.Generic;

namespace Gogos
{
    public enum RangeTier { Disabled, Small, Medium, Large }

    public class RangeTierTracker : AbstractTierTracker<RangeTier>
    {
        public override TierVariant TierVariant => TierVariant.Range;
        public float Range => RangesByTier[Tier];

        private static readonly Dictionary<RangeTier, float> RangesByTier = new Dictionary<RangeTier, float>()
        {
            { RangeTier.Disabled, 0 },
            { RangeTier.Small, 20 },
            { RangeTier.Medium, 30 },
            { RangeTier.Large, 40 },
        };
    }
}
