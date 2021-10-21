using System.Collections.Generic;

namespace Gogos
{
    public enum PointValueTier { Low, Medium, High, Immense }

    public class PointValueTierTracker : AbstractTierTracker<PointValueTier>
    {
        public override TierVariant TierVariant => TierVariant.PointValue;

        public int PointValue => PointValuesByTier[Tier];

        private static readonly Dictionary<PointValueTier, int> PointValuesByTier = new Dictionary<PointValueTier, int>()
        {
            { PointValueTier.Low, 100 },
            { PointValueTier.Medium, 200 },
            { PointValueTier.High, 400 },
            { PointValueTier.Immense, 1000 },
        };
    }
}
