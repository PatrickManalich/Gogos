using UnityEngine;

namespace Gogos
{
    public class TriggerRangeRefresher : MonoBehaviour
    {
        [SerializeField]
        private RangeTierTracker m_RangeTierTracker;

        [SerializeField]
        private GameObject m_TriggerRange;

        private void OnEnable()
        {
            m_RangeTierTracker.TierChanged += Refresh;

            Refresh();
        }

        private void OnDisable()
        {
            m_RangeTierTracker.TierChanged -= Refresh;
        }

        private void Refresh()
        {
            var triggerRangeParent = m_TriggerRange.transform.parent;
            m_TriggerRange.transform.parent = null; // Work around for changing global scale
            m_TriggerRange.transform.localScale = transform.localScale * m_RangeTierTracker.Range;
            m_TriggerRange.transform.parent = triggerRangeParent;
        }
    }
}
