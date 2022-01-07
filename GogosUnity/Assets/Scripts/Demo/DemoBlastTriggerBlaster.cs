using UnityEngine;

namespace Gogos
{
    public class DemoBlastTriggerBlaster : MonoBehaviour
    {
        [SerializeField]
        private RangeTierTracker m_RangeTierTracker;

        [SerializeField]
        private BlastPowerTierTracker m_BlastPowerTierTracker;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

        private void Start()
        {
            m_BlastTrigger.Blast(m_RangeTierTracker, m_BlastPowerTierTracker);
        }
    }
}
