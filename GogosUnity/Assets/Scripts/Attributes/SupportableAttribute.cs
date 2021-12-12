using UnityEngine;

namespace Gogos
{
    public class SupportableAttribute : MonoBehaviour
    {
        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private TierTrackerReference m_TierTrackerReference;

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
                foreach (var tierTracker in m_TierTrackerReference.TierTrackers)
                {
                    supportTrigger.SupportAbility.ProvideSupport(tierTracker);
                }
            }
        }

        private void TriggerListener_OnExited(object sender, TriggerEventArgs e)
        {
            var supportTrigger = e.OtherCollider.GetComponent<SupportTrigger>();
            if (supportTrigger != null)
            {
                foreach (var tierTracker in m_TierTrackerReference.TierTrackers)
                {
                    supportTrigger.SupportAbility.RemoveSupport(tierTracker);
                }
            }
        }
    }
}
