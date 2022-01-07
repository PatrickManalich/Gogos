using System.Collections.Generic;

namespace Gogos
{
    public enum BlastPowerTier { Weak, Medium, Strong }

    public class BlastPowerTierTracker : AbstractTierTracker<BlastPowerTier>
    {
        public override TierVariant TierVariant => TierVariant.BlastPower;

        public float BlastOutwardPower => BlastOutwardPowersByTier[Tier];
        public float BlastUpwardPower => BlastUpwardPowersByTier[Tier];

        private static readonly Dictionary<BlastPowerTier, float> BlastOutwardPowersByTier = new Dictionary<BlastPowerTier, float>()
        {
            { BlastPowerTier.Weak, 10 },
            { BlastPowerTier.Medium, 15 },
            { BlastPowerTier.Strong, 20 },
        };

        private static readonly Dictionary<BlastPowerTier, float> BlastUpwardPowersByTier = new Dictionary<BlastPowerTier, float>()
        {
            { BlastPowerTier.Weak, 15 },
            { BlastPowerTier.Medium, 20 },
            { BlastPowerTier.Strong, 25 },
        };
    }
}
