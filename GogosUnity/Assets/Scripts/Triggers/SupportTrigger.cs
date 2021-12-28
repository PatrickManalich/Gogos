using UnityEngine;

namespace Gogos
{
    public class SupportTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public SupportAbility SupportAbility => m_SupportAbility;

        [SerializeField]
        private SupportAbility m_SupportAbility;

        [SerializeField]
        private Animator m_SupportTriggerAnimator;

        private const string ExpandName = "Expand";

        public void ProvideSupport()
        {
            m_SupportTriggerAnimator.SetBool(ExpandName, true);
        }

        public void RemoveSupport()
        {
            m_SupportTriggerAnimator.SetBool(ExpandName, false);
        }
    }
}
