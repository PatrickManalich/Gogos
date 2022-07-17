namespace Gogos
{
    public class ShieldTrigger : ExpandTrigger
    {
        public RangeTierTracker RangeTierTracker { get; private set; }

        public ShieldDurabilityTierTracker ShieldDurabilityTierTracker { get; private set; }

        public ShieldAbility ShieldAbility { get; private set; }

        public void EnableShield(RangeTierTracker rangeTierTracker, ShieldDurabilityTierTracker shieldDurabilityTierTracker, ShieldAbility shieldAbility)
        {
            RangeTierTracker = rangeTierTracker;
            ShieldDurabilityTierTracker = shieldDurabilityTierTracker;
            ShieldAbility = shieldAbility;
            Expand();
        }

        public void DisableShield()
        {
            Shrink();
        }
    }
}
