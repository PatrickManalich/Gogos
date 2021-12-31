using System.Collections.Generic;

namespace Gogos
{
    public enum ShieldStrengthTier { Broken, Weak, Medium, Strong }

    public class ShieldStrengthTierTracker : AbstractTierTracker<ShieldStrengthTier>
    {
        public override TierVariant TierVariant => TierVariant.ShieldStrength;

        public int LastTurnModified { get; private set; }
        public bool IsShieldBroken => Tier == ShieldStrengthTier.Broken;
        public float ShieldAlpha => ShieldAlphasByTier[Tier];

        public override void ModifyTier(int modifier)
        {
            base.ModifyTier(modifier);
            LastTurnModified = TurnTracker.Turn;
        }

        private static readonly Dictionary<ShieldStrengthTier, float> ShieldAlphasByTier = new Dictionary<ShieldStrengthTier, float>()
        {
            { ShieldStrengthTier.Broken, 0 },
            { ShieldStrengthTier.Weak, 0.1f },
            { ShieldStrengthTier.Medium, 0.2f },
            { ShieldStrengthTier.Strong, 0.3f },
        };
    }
}
