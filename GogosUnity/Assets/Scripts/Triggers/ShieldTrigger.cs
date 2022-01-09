using UnityEngine;

namespace Gogos
{
    public class ShieldTrigger : MonoBehaviour
    {
        public Player Player { get; set; }

        public RangeTierTracker RangeTierTracker { get; private set; }

        public ShieldStrengthTierTracker ShieldStrengthTierTracker { get; private set; }

        public ShieldAbility ShieldAbility { get; private set; }

        public Vector3 CenterPosition => m_CenterPoint.transform.position;

        [SerializeField]
        private Collider m_Collider;

        [SerializeField]
        private Animator m_ShieldTriggerAnimator;

        [SerializeField]
        private GameObject m_CenterPoint;

        private const string ExpandName = "Expand";

        private void Awake()
        {
            m_Collider.enabled = false;
        }

        public void EnableShield(RangeTierTracker rangeTierTracker, ShieldStrengthTierTracker shieldStrengthTierTracker, ShieldAbility shieldAbility)
        {
            RangeTierTracker = rangeTierTracker;
            ShieldStrengthTierTracker = shieldStrengthTierTracker;
            ShieldAbility = shieldAbility;
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
