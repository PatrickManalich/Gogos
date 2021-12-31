using System.Collections.Generic;

namespace Gogos
{
    public enum CapacityTier { Low, Medium, High }

    public class CapacityTierTracker : AbstractTierTracker<CapacityTier>
    {
        public override TierVariant TierVariant => TierVariant.Capacity;

        public int Capacity => CapacitiesByTier[Tier];

        private static readonly Dictionary<CapacityTier, int> CapacitiesByTier = new Dictionary<CapacityTier, int>()
        {
            { CapacityTier.Low, 4 },
            { CapacityTier.Medium, 6 },
            { CapacityTier.High, 8 },
        };
    }
}
