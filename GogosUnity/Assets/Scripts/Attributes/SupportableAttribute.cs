using UnityEngine;

namespace Gogos
{
    public class SupportableAttribute : MonoBehaviour
    {
        public Player Player { get; set; }

        public bool IsSupported => m_SupportProviderCount > 0;

        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private TierTrackerReference m_TierTrackerReference;

        private int m_SupportProviderCount;

        private void Awake()
        {
            m_TriggerListener.Entered += TriggerListener_OnEntered;
            m_TriggerListener.Exited += TriggerListener_OnExited;
        }

        private void OnDestroy()
        {
            m_TriggerListener.Exited -= TriggerListener_OnExited;
            m_TriggerListener.Entered -= TriggerListener_OnEntered;
        }

        private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
        {
            var supportTrigger = e.OtherCollider.GetComponent<SupportTrigger>();
            if (supportTrigger != null)
            {
                var supportAbility = supportTrigger.SupportAbility;
                foreach (var tierTracker in m_TierTrackerReference.TierTrackers)
                {
                    if (supportAbility.CanSupport(Player, tierTracker))
                    {
                        supportAbility.ProvideSupport(tierTracker);
                        m_SupportProviderCount++;
                    }
                }
            }
        }

        private void TriggerListener_OnExited(object sender, TriggerEventArgs e)
        {
            var supportTrigger = e.OtherCollider.GetComponent<SupportTrigger>();
            if (supportTrigger != null)
            {
                var supportAbility = supportTrigger.SupportAbility;
                foreach (var tierTracker in m_TierTrackerReference.TierTrackers)
                {
                    if (supportAbility.CanSupport(Player, tierTracker))
                    {
                        supportAbility.RemoveSupport(tierTracker);
                        m_SupportProviderCount--;
                    }
                }
            }
        }
    }
}
