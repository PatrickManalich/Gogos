using System.Collections.Generic;

namespace Gogos
{
	public enum ShieldStrengthTier { Broken, Weak, Medium, Strong }

	public class ShieldStrengthTierTracker : AbstractTierTracker<ShieldStrengthTier>
	{
        public override TierVariant TierVariant => TierVariant.ShieldStrength;

        public bool IsShieldBroken => Tier == ShieldStrengthTier.Broken;
        public float ShieldAlpha => ShieldAlphaByTier[Tier];

        private static readonly Dictionary<ShieldStrengthTier, float> ShieldAlphaByTier = new Dictionary<ShieldStrengthTier, float>()
        {
            { ShieldStrengthTier.Broken, 0 },
            { ShieldStrengthTier.Weak, 0.1f },
            { ShieldStrengthTier.Medium, 0.2f },
            { ShieldStrengthTier.Strong, 0.3f },
        };
    }
}