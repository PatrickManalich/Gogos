namespace Gogos
{
    public enum RarityTier { Common, Uncommon, Rare, Golden }

    public class RarityTierTracker : AbstractTierTracker<RarityTier>
    {
        public override TierVariant TierVariant => TierVariant.Rarity;
    }
}