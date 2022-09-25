using System.Collections;
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

        [SerializeField]
        private float m_BlastDelay;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(m_BlastDelay);
            m_BlastTrigger.Blast(m_RangeTierTracker, m_BlastPowerTierTracker);
        }
    }
}
