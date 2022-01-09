using UnityEngine;

namespace Gogos
{
    public class BlastTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public RangeTierTracker RangeTierTracker { get; private set; }

        public BlastPowerTierTracker BlastPowerTierTracker { get; private set; }

        public Vector3 CenterPosition => m_CenterPoint.transform.position;

        [SerializeField]
        private Collider m_Collider;

        [SerializeField]
        private Animator m_BlastTriggerAnimator;

        [SerializeField]
        private GameObject m_CenterPoint;

        private const string ExpandName = "Expand";

        private void Awake()
        {
            m_Collider.enabled = false;
        }

        public void Blast(RangeTierTracker rangeTierTracker, BlastPowerTierTracker blastPowerTierTracker)
        {
            RangeTierTracker = rangeTierTracker;
            BlastPowerTierTracker = blastPowerTierTracker;
            m_Collider.enabled = true;
            m_BlastTriggerAnimator.SetTrigger(ExpandName);
        }
    }
}
