using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class BombBlastedEffect : AbstractBlastedEffect, ITriggerAnimationObserver
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
        private BlastForceTierTracker m_BlastForceTierTracker;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

        [SerializeField]
        private TriggerAnimationSubject m_TriggerAnimationSubject;

        public void Notify()
        {
            m_TriggerAnimationSubject.RemoveObserverForAnimationFinished(this, TriggerAnimation.Expand);
            Destroy(m_Rigidbody.gameObject);
        }

        protected override void OnBlasted()
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            m_Visuals.SetActive(false);
            m_TriggerRangeRefresher.enabled = false;

            m_TriggerAnimationSubject.AddObserverForAnimationFinished(this, TriggerAnimation.Expand);
            m_BlastTrigger.Blast(m_RangeTierTracker, m_BlastForceTierTracker);
        }
    }
}
