namespace Gogos
{
    public enum ShieldDurabilityTier { Broken, Weak, Medium, Strong, Unbreakable }

    public class ShieldDurabilityTierTracker : AbstractTierTracker<ShieldDurabilityTier>
    {
        public override TierVariant TierVariant => TierVariant.ShieldDurability;

        public int LastTurnModified { get; private set; }
        public bool IsShieldBroken => Tier == ShieldDurabilityTier.Broken;
        public bool IsShieldUnbreakable => Tier == ShieldDurabilityTier.Unbreakable;

        public override void ModifyTier(int modifier)
        {
            base.ModifyTier(modifier);
            LastTurnModified = TurnTracker.Turn;
        }
    }
}
