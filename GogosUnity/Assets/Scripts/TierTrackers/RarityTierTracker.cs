namespace Gogos
{
    public enum RarityTier { Common, Rare, Epic, Golden }

    public class RarityTierTracker : AbstractTierTracker<RarityTier>
    {
        public override TierVariant TierVariant => TierVariant.Rarity;
    }
}
