using UnityEngine;

namespace Gogos
{
    public class BlastTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public BlastForceTierTracker BlastForceTierTracker { get; private set; }

        [SerializeField]
        private Animator m_BlastTriggerAnimator;

        private const string ExpandName = "Expand";

        public void Blast(BlastForceTierTracker blastForceTierTracker)
        {
            BlastForceTierTracker = blastForceTierTracker;
            m_BlastTriggerAnimator.SetTrigger(ExpandName);
        }
    }
}
