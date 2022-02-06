namespace Gogos
{
    public enum ShieldStrengthTier { Broken, Weak, Medium, Strong, Unbreakable }

    public class ShieldStrengthTierTracker : AbstractTierTracker<ShieldStrengthTier>
    {
        public override TierVariant TierVariant => TierVariant.ShieldStrength;

        public int LastTurnModified { get; private set; }
        public bool IsShieldBroken => Tier == ShieldStrengthTier.Broken;
        public bool IsShieldUnbreakable => Tier == ShieldStrengthTier.Unbreakable;

        public override void ModifyTier(int modifier)
        {
            base.ModifyTier(modifier);
            LastTurnModified = TurnTracker.Turn;
        }
    }
}
