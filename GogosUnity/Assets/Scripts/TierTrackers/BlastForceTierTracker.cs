using System.Collections.Generic;

namespace Gogos
{
    public enum BlastForceTier { Weak, Medium, Strong }

    public class BlastForceTierTracker: AbstractTierTracker<BlastForceTier>
	{
        public override TierVariant TierVariant => TierVariant.BlastForce;
       
        public float BlastForce => BlastForcesByTier[Tier];
        public float BlastUpwardsModifier => BlastUpwardsModifiersByTier[Tier];
        
        private static readonly Dictionary<BlastForceTier, float> BlastForcesByTier = new Dictionary<BlastForceTier, float>()
        {
            { BlastForceTier.Weak, 7 },
            { BlastForceTier.Medium, 9 },
            { BlastForceTier.Strong, 11 },
        };
        private static readonly Dictionary<BlastForceTier, float> BlastUpwardsModifiersByTier = new Dictionary<BlastForceTier, float>()
        {
            { BlastForceTier.Weak, 2 },
            { BlastForceTier.Medium, 2 },
            { BlastForceTier.Strong, 2 },
        };
    }
}