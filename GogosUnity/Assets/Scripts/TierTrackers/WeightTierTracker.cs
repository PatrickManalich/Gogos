using System.Collections.Generic;

namespace Gogos
{
    public enum WeightTier { Lightweight, Middleweight, Heavyweight }

    public class WeightTierTracker : AbstractTierTracker<WeightTier>
    {
        public override TierVariant TierVariant => TierVariant.Weight;
        public float Weight => WeightsByTier[Tier];

        private static readonly Dictionary<WeightTier, float> WeightsByTier = new Dictionary<WeightTier, float>()
        {
            { WeightTier.Lightweight, 0.75f },
            { WeightTier.Middleweight, 1 },
            { WeightTier.Heavyweight, 1.25f },
        };
    }
}