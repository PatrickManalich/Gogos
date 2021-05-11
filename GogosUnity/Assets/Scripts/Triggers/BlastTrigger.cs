using UnityEngine;

namespace Gogos
{
	public class BlastTrigger : MonoBehaviour
	{
        public BlastForceTierTracker BlastForceTierTracker => m_BlastForceTierTracker;

        [SerializeField]
        private BlastForceTierTracker m_BlastForceTierTracker;

        [SerializeField]
        private Animator m_BlastTriggerAnimator;

        private const string ExpandName = "Expand";

        public void Blast()
        {
            m_BlastTriggerAnimator.SetTrigger(ExpandName);
        }

    }
}