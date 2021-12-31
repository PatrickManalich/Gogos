using UnityEngine;

namespace Gogos
{
    public class SupportTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public SupportAbility SupportAbility { get; private set; }

        [SerializeField]
        private Collider m_Collider;

        [SerializeField]
        private Animator m_SupportTriggerAnimator;

        private const string ExpandName = "Expand";

        private void Awake()
        {
            m_Collider.enabled = false;
        }

        public void ProvideSupport(SupportAbility supportAbility)
        {
            SupportAbility = supportAbility;
            m_Collider.enabled = true;
            m_SupportTriggerAnimator.SetBool(ExpandName, true);
        }

        public void RemoveSupport()
        {
            m_Collider.enabled = false;
            m_SupportTriggerAnimator.SetBool(ExpandName, false);
        }
    }
}
