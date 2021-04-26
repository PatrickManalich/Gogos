using UnityEngine;

namespace Gogos
{
	public class BlastTrigger : MonoBehaviour
	{
        public BlastForceTierTracker BlastForceTierTracker => m_BlastForceTierTracker;

        [SerializeField]
        private BlastForceTierTracker m_BlastForceTierTracker;
    }
}