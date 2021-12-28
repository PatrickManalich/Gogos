using UnityEngine;

namespace Gogos
{
    public class ShieldTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        [SerializeField]
        private Animator m_ShieldTriggerAnimator;

        private const string ExpandName = "Expand";

        public void EnableShield()
        {
            m_ShieldTriggerAnimator.SetBool(ExpandName, true);
        }

        public void DisableShield()
        {
            m_ShieldTriggerAnimator.SetBool(ExpandName, false);
        }
    }
}
