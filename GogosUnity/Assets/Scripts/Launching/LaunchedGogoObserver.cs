using System;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class LaunchedGogoObserver : MonoBehaviour
    {
        public event Action StartedExpanding;

        public event Action Expanded;

        public event Action Collected;

        [SerializeField]
        private Launcher m_Launcher;

        private TriggerAnimationsReference m_TriggerAnimationsReference;
        private CollectableAttribute m_CollectableAttribute;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            ClearComponentsAndUnsubscribe();
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Launching)
            {
                var launcherProjectile = m_Launcher.ProjectileRigidbody.gameObject;
                GetComponentsAndSubscribe(launcherProjectile);
            }
        }

        private void TriggerAnimationsReference_OnAnimationStarted(object sender, TriggerAnimationEventArgs e)
        {
            if (e.TriggerAnimation == TriggerAnimation.Expand)
            {
                StartedExpanding?.Invoke();
            }
        }

        private void TriggerAnimationsReference_OnAnimationFinished(object sender, TriggerAnimationEventArgs e)
        {
            if (e.TriggerAnimation == TriggerAnimation.Expand)
            {
                ClearComponentsAndUnsubscribe();
                StartCoroutine(InvokeExpandedAfterDelayRoutine());
            }
        }

        private void CollectableAttribute_OnCollected()
        {
            ClearComponentsAndUnsubscribe();
            Collected?.Invoke();
        }

        private void GetComponentsAndSubscribe(GameObject launcherProjectile)
        {
            m_TriggerAnimationsReference = launcherProjectile.GetComponentInChildren<TriggerAnimationsReference>();
            m_TriggerAnimationsReference.AnimationStarted += TriggerAnimationsReference_OnAnimationStarted;
            m_TriggerAnimationsReference.AnimationFinished += TriggerAnimationsReference_OnAnimationFinished;

            m_CollectableAttribute = launcherProjectile.GetComponentInChildren<CollectableAttribute>();
            m_CollectableAttribute.Collected += CollectableAttribute_OnCollected;
        }

        private void ClearComponentsAndUnsubscribe()
        {
            if (m_TriggerAnimationsReference != null)
            {
                m_TriggerAnimationsReference.AnimationFinished -= TriggerAnimationsReference_OnAnimationFinished;
                m_TriggerAnimationsReference.AnimationStarted -= TriggerAnimationsReference_OnAnimationStarted;
                m_TriggerAnimationsReference = null;
            }

            if (m_CollectableAttribute != null)
            {
                m_CollectableAttribute.Collected -= CollectableAttribute_OnCollected;
                m_CollectableAttribute = null;
            }
        }

        private IEnumerator InvokeExpandedAfterDelayRoutine()
        {
            yield return new WaitForSeconds(2);
            Expanded?.Invoke();
        }
    }
}
