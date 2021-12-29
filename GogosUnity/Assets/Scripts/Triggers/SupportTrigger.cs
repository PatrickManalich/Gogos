using UnityEngine;

namespace Gogos
{
    public class SupportTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public SupportAbility SupportAbility { get; private set; }

        [SerializeField]
        private Animator m_SupportTriggerAnimator;

        private const string ExpandName = "Expand";

        public void ProvideSupport(SupportAbility supportAbility)
        {
            SupportAbility = supportAbility;
            m_SupportTriggerAnimator.SetBool(ExpandName, true);
        }

        public void RemoveSupport()
        {
            m_SupportTriggerAnimator.SetBool(ExpandName, false);
        }
    }
}
