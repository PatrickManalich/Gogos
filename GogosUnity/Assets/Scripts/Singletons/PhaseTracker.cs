using System;
using UnityEngine;

namespace Gogos
{
    public enum Phase { Returning, Selecting, Launching, Settling, Transitioning }

    public class PhaseTracker : AbstractSingleton<PhaseTracker>, ITriggerAnimationObserver
    {
        public static event Action PhaseChanged;

        public static Phase Phase { get; private set; }

        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private AccelerometerSettlingWatcher m_AccelerometerSettlingWatcher;

        [SerializeField]
        private PlayerGogoReturner m_PlayerGogoReturner;

        private TriggerAnimationSubject m_TriggerAnimationSubject;

        protected override void Awake()
        {
            base.Awake();
            Phase = Phase.Selecting;
        }

        private void Start()
        {
            m_Launcher.Launched += Launcher_OnLaunched;
            m_AccelerometerSettlingWatcher.Settled += AccelerometerSettlingWatcher_OnSettled;
            PlayerTracker.PlayerChanged += PlayerTracker_OnPlayerChanged;
            m_PlayerGogoReturner.Returned += PlayerGogoReturner_OnReturned;
        }

        private void OnDestroy()
        {
            m_PlayerGogoReturner.Returned -= PlayerGogoReturner_OnReturned;
            PlayerTracker.PlayerChanged -= PlayerTracker_OnPlayerChanged;
            m_AccelerometerSettlingWatcher.Settled -= AccelerometerSettlingWatcher_OnSettled;
            m_Launcher.Launched -= Launcher_OnLaunched;
        }

        public void Notify()
        {
            m_TriggerAnimationSubject.RemoveObserverForAnimationFinished(this, TriggerAnimation.Expand);
            m_TriggerAnimationSubject = null;

            Phase = Phase.Settling;
            PhaseChanged?.Invoke();
        }

        private void Launcher_OnLaunched()
        {
            m_TriggerAnimationSubject = m_Launcher.Projectile.GetComponentInChildren<TriggerAnimationSubject>();
            m_TriggerAnimationSubject.AddObserverForAnimationFinished(this, TriggerAnimation.Expand);

            Phase = Phase.Launching;
            PhaseChanged?.Invoke();
        }

        private void AccelerometerSettlingWatcher_OnSettled()
        {
            Phase = Phase.Transitioning;
            PhaseChanged?.Invoke();
        }

        private void PlayerTracker_OnPlayerChanged()
        {
            Phase = Phase.Returning;
            PhaseChanged?.Invoke();
        }

        private void PlayerGogoReturner_OnReturned()
        {
            Phase = Phase.Selecting;
            PhaseChanged?.Invoke();
        }
    }
}
