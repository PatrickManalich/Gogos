using System;
using UnityEngine;

namespace Gogos
{
    public class LaunchedGogoObserver : MonoBehaviour, ITriggerAnimationObserver
    {
        public event Action Expanded;

        [SerializeField]
        private Launcher m_Launcher;

        private TriggerAnimationSubject m_TriggerAnimationSubject;

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
            Expanded?.Invoke();
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Launching)
            {
                m_TriggerAnimationSubject = m_Launcher.Projectile.GetComponentInChildren<TriggerAnimationSubject>();
                m_TriggerAnimationSubject.AddObserverForAnimationFinished(this, TriggerAnimation.Expand);
            }
        }
    }
}
