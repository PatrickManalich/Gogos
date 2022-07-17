using UnityEngine;

namespace Gogos
{
    public class GoldenMosh : AbstractGogo
    {
        [SerializeField]
        private ShieldAbility m_ShieldAbility;

        [SerializeField]
        private GroundSnapper m_TriggerRangeGroundSnapper;

        [SerializeField]
        private ShieldTrigger m_ShieldTrigger;

        private RangeTierTracker m_RangeTierTracker;
        private ShieldDurabilityTierTracker m_ShieldDurabilityTierTracker;

        protected override void Start()
        {
            base.Start();
            m_RangeTierTracker = (RangeTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            m_ShieldDurabilityTierTracker = (ShieldDurabilityTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldDurability);
        }

        protected override void OnStartedMoving()
        {
            ReparentTriggerRange();
            m_ShieldTrigger.DisableShield();
        }

        protected override void OnStoppedMoving()
        {
            if (m_ShieldDurabilityTierTracker.IsShieldBroken)
            {
                return;
            }

            m_TriggerRangeGroundSnapper.SnapToGround();
            UnparentTriggerRange();
            m_ShieldTrigger.EnableShield(m_RangeTierTracker, m_ShieldDurabilityTierTracker, m_ShieldAbility);
        }
    }
}
