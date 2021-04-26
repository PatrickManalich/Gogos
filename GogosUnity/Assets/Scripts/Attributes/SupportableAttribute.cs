using UnityEngine;

namespace Gogos
{
    public class SupportableAttribute : MonoBehaviour
    {
        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        private AbstractTierTracker[] m_TierTrackers;

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
            if (supportTrigger)
            {
                foreach (var tierTracker in m_TierTrackers)
                {
                    supportTrigger.SupportAbility.ProvideSupport(tierTracker);
                }
            }
        }

        private void TriggerListener_OnExited(object sender, TriggerEventArgs e)
        {
            var supportTrigger = e.OtherCollider.GetComponent<SupportTrigger>();
            if (supportTrigger)
            {
                foreach (var tierTracker in m_TierTrackers)
                {
                    supportTrigger.SupportAbility.RemoveSupport(tierTracker);
                }
            }
        }
    }
}