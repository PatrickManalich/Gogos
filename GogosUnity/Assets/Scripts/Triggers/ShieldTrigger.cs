namespace Gogos
{
    public class ShieldTrigger : AnimatedTrigger
    {
        public RangeTierTracker RangeTierTracker { get; private set; }

        public ShieldStrengthTierTracker ShieldStrengthTierTracker { get; private set; }

        public ShieldAbility ShieldAbility { get; private set; }

        public void EnableShield(RangeTierTracker rangeTierTracker, ShieldStrengthTierTracker shieldStrengthTierTracker, ShieldAbility shieldAbility)
        {
            RangeTierTracker = rangeTierTracker;
            ShieldStrengthTierTracker = shieldStrengthTierTracker;
            ShieldAbility = shieldAbility;
            Expand();
        }

        public void DisableShield()
        {
            Shrink();
        }
    }
}
