using System.Collections.Generic;

namespace Gogos
{
    public enum BlastForceTier { Weak, Medium, Strong }

    public class BlastForceTierTracker : AbstractTierTracker<BlastForceTier>
    {
        public override TierVariant TierVariant => TierVariant.BlastForce;

        public float BlastForce => BlastForcesByTier[Tier];
        public float BlastUpwardsModifier => BlastUpwardsModifiersByTier[Tier];

        private static readonly Dictionary<BlastForceTier, float> BlastForcesByTier = new Dictionary<BlastForceTier, float>()
        {
            { BlastForceTier.Weak, 10 },
            { BlastForceTier.Medium, 15 },
            { BlastForceTier.Strong, 20 },
        };

        private static readonly Dictionary<BlastForceTier, float> BlastUpwardsModifiersByTier = new Dictionary<BlastForceTier, float>()
        {
            { BlastForceTier.Weak, 15 },
            { BlastForceTier.Medium, 20 },
            { BlastForceTier.Strong, 25 },
        };
    }
}
