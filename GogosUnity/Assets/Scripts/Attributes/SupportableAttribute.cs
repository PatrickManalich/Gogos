using UnityEngine;

namespace Gogos
{
    public class SupportableAttribute : AbstractAttribute
    {
        public bool IsSupported => m_SupportProviderCount > 0;

        [SerializeField]
        private TierTrackerReference m_TierTrackerReference;

        private int m_SupportProviderCount;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var supportTrigger = e.OtherCollider.GetComponent<SupportTrigger>();
            if (supportTrigger == null)
            {
                return;
            }

            var supportAbility = supportTrigger.SupportAbility;
            foreach (var tierTracker in m_TierTrackerReference.TierTrackers)
            {
                if (supportAbility.CanSupport(GroupTag, Player, tierTracker))
                {
                    supportAbility.ProvideSupport(tierTracker);
                    m_SupportProviderCount++;
                }
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        {
            var supportTrigger = e.OtherCollider.GetComponent<SupportTrigger>();
            if (supportTrigger != null)
            {
                var supportAbility = supportTrigger.SupportAbility;
                foreach (var tierTracker in m_TierTrackerReference.TierTrackers)
                {
                    if (supportAbility.CanSupport(GroupTag, Player, tierTracker))
                    {
                        supportAbility.RemoveSupport(tierTracker);
                        m_SupportProviderCount--;
                    }
                }
            }
        }
    }
}
