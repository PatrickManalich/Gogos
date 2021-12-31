using UnityEngine;

namespace Gogos
{
    public class BlastTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public BlastForceTierTracker BlastForceTierTracker { get; private set; }

        [SerializeField]
        private Collider m_Collider;

        [SerializeField]
        private Animator m_BlastTriggerAnimator;

        private const string ExpandName = "Expand";

        private void Awake()
        {
            m_Collider.enabled = false;
        }

        public void Blast(BlastForceTierTracker blastForceTierTracker)
        {
            BlastForceTierTracker = blastForceTierTracker;
            m_Collider.enabled = true;
            m_BlastTriggerAnimator.SetTrigger(ExpandName);
        }
    }
}
