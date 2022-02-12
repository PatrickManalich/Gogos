using UnityEngine;

namespace Gogos
{
    public class GoldenSato : AbstractGogo
    {
        [SerializeField]
        private TriggerRangeRefresher m_TriggerRangeRefresher;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

        private bool m_CounterBlastPrepped;
        private Vector3 m_CounterBlastTargetPosition;

        public void PrepareCounterBlast(Vector3 counterBlastTargetPosition)
        {
            m_CounterBlastTargetPosition = counterBlastTargetPosition;
            m_CounterBlastPrepped = true;
        }

        protected override void OnStartedMoving()
        {
            m_TriggerRangeRefresher.enabled = true;
        }

        protected override void OnStoppedMoving()
        {
            if (!m_CounterBlastPrepped)
            {
                return;
            }

            m_TriggerRangeRefresher.enabled = false;
            ReparentTriggerRange();
            UnparentTriggerRange();
            TriggerRange.transform.LookAt(m_CounterBlastTargetPosition);

            var rangeTierTracker = (RangeTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            var blastPowerTierTracker = (BlastPowerTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastPower);
            m_BlastTrigger.Blast(rangeTierTracker, blastPowerTierTracker);

            m_CounterBlastTargetPosition = Vector3.zero;
            m_CounterBlastPrepped = false;
        }
    }
}
