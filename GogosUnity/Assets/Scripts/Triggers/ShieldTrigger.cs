using UnityEngine;

namespace Gogos
{
    public class ShieldTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public ShieldStrengthTierTracker ShieldStrengthTierTracker { get; private set; }

        public ShieldAbility ShieldAbility { get; private set; }

        public Vector3 CenterPosition { get; private set; }

        [SerializeField]
        private Collider m_Collider;

        [SerializeField]
        private Animator m_ShieldTriggerAnimator;

        private const string ExpandName = "Expand";

        private void Awake()
        {
            m_Collider.enabled = false;
        }

        public void EnableShield(ShieldStrengthTierTracker shieldStrengthTierTracker, ShieldAbility shieldAbility)
        {
            ShieldStrengthTierTracker = shieldStrengthTierTracker;
            ShieldAbility = shieldAbility;
            CenterPosition = transform.position;
            m_Collider.enabled = true;
            m_ShieldTriggerAnimator.SetBool(ExpandName, true);
        }

        public void DisableShield()
        {
            m_Collider.enabled = false;
            m_ShieldTriggerAnimator.SetBool(ExpandName, false);
        }
    }
}
