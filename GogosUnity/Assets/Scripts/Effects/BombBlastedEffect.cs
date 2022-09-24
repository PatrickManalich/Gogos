using UnityEngine;

namespace Gogos
{
    public class BombBlastedEffect : AbstractBlastedEffect
    {
        [SerializeField]
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private GameObject m_Visuals;

        [SerializeField]
        private TriggerRangeRefresher m_TriggerRangeRefresher;

        [SerializeField]
        private RangeTierTracker m_RangeTierTracker;

        [SerializeField]
        private BlastPowerTierTracker m_BlastPowerTierTracker;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

        [SerializeField]
        private TriggerAnimationsReference m_TriggerAnimationsReference;

        protected override void Start()
        {
            base.Start();
            m_TriggerAnimationsReference.AnimationFinished += TriggerAnimationsReference_OnAnimationFinished;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_TriggerAnimationsReference.AnimationFinished -= TriggerAnimationsReference_OnAnimationFinished;
        }

        protected override void OnBlasted(BlastTriggerEventArgs e)
        {
            UnsubscribeFromBlastedEvent();

            m_Rigidbody.transform.rotation = Quaternion.identity;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            m_Visuals.SetActive(false);
            m_TriggerRangeRefresher.enabled = false;

            m_BlastTrigger.Blast(m_RangeTierTracker, m_BlastPowerTierTracker);
        }

        private void TriggerAnimationsReference_OnAnimationFinished(object sender, TriggerAnimationEventArgs e)
        {
            if (e.TriggerAnimation == TriggerAnimation.Expand)
            {
                Destroy(m_Rigidbody.gameObject);
            }
        }
    }
}
