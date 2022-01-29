namespace Gogos
{
    public class BlastTrigger : AnimatedTrigger
    {
        public RangeTierTracker RangeTierTracker { get; private set; }

        public BlastPowerTierTracker BlastPowerTierTracker { get; private set; }

        public void Blast(RangeTierTracker rangeTierTracker, BlastPowerTierTracker blastPowerTierTracker)
        {
            RangeTierTracker = rangeTierTracker;
            BlastPowerTierTracker = blastPowerTierTracker;
            Expand();
        }
    }
}
