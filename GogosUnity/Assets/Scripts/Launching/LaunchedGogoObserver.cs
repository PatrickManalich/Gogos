using System;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class LaunchedGogoObserver : MonoBehaviour, ITriggerAnimationObserver
    {
        public event Action Expanded;

        public event Action Collected;

        [SerializeField]
        private Launcher m_Launcher;

        private TriggerAnimationSubject m_TriggerAnimationSubject;
        private CollectableAttribute m_CollectableAttribute;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        public void Notify()
        {
            m_TriggerAnimationSubject.RemoveObserverForAnimationFinished(this, TriggerAnimation.Expand);
            m_TriggerAnimationSubject = null;
            m_CollectableAttribute.Collected -= CollectableAttribute_OnCollected;
            m_CollectableAttribute = null;

            StartCoroutine(InvokeExpandedAfterDelayRoutine());
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Launching)
            {
                var launcherProjectile = m_Launcher.ProjectileRigidbody.gameObject;
                m_TriggerAnimationSubject = launcherProjectile.GetComponentInChildren<TriggerAnimationSubject>();
                m_TriggerAnimationSubject.AddObserverForAnimationFinished(this, TriggerAnimation.Expand);

                m_CollectableAttribute = launcherProjectile.GetComponentInChildren<CollectableAttribute>();
                m_CollectableAttribute.Collected += CollectableAttribute_OnCollected;
            }
        }

        private void CollectableAttribute_OnCollected()
        {
            m_TriggerAnimationSubject.RemoveObserverForAnimationFinished(this, TriggerAnimation.Expand);
            m_TriggerAnimationSubject = null;
            m_CollectableAttribute.Collected -= CollectableAttribute_OnCollected;
            m_CollectableAttribute = null;

            Collected?.Invoke();
        }

        private IEnumerator InvokeExpandedAfterDelayRoutine()
        {
            yield return new WaitForSeconds(2);
            Expanded?.Invoke();
        }
    }
}
