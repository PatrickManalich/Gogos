namespace Gogos
{
    public enum PointValueTier { Low, Medium, High, Immense }

    public class PointValueTierTracker : AbstractTierTracker<PointValueTier>
    {
        public override TierVariant TierVariant => TierVariant.PointValue;
    }
}