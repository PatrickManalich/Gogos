using System;
using UnityEngine;

namespace Gogos
{
    public enum Phase { Returning, Selecting, Launching, Settling, Transitioning }

    public class PhaseTracker : AbstractSingleton<PhaseTracker>
    {
        public static event Action PhaseChanged;

        public static Phase Phase { get; private set; }

        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private LaunchedGogoObserver m_LaunchedGogoObserver;

        [SerializeField]
        private AccelerometerSettlingWatcher m_AccelerometerSettlingWatcher;

        [SerializeField]
        private PlayerGogoReturner m_PlayerGogoReturner;

        protected override void Awake()
        {
            base.Awake();
            Phase = Phase.Selecting;
        }

        private void Start()
        {
            m_Launcher.Launched += Launcher_OnLaunched;
            m_LaunchedGogoObserver.Expanded += LaunchedGogoObserver_OnExpanded;
            m_AccelerometerSettlingWatcher.Settled += AccelerometerSettlingWatcher_OnSettled;
            PlayerTracker.PlayerChanged += PlayerTracker_OnPlayerChanged;
            m_PlayerGogoReturner.Returned += PlayerGogoReturner_OnFinished;
            m_PlayerGogoReturner.Skipped += PlayerGogoReturner_OnFinished;
        }

        private void OnDestroy()
        {
            m_PlayerGogoReturner.Skipped -= PlayerGogoReturner_OnFinished;
            m_PlayerGogoReturner.Returned -= PlayerGogoReturner_OnFinished;
            PlayerTracker.PlayerChanged -= PlayerTracker_OnPlayerChanged;
            m_AccelerometerSettlingWatcher.Settled -= AccelerometerSettlingWatcher_OnSettled;
            m_LaunchedGogoObserver.Expanded -= LaunchedGogoObserver_OnExpanded;
            m_Launcher.Launched -= Launcher_OnLaunched;
        }

        private void Launcher_OnLaunched()
        {
            Phase = Phase.Launching;
            PhaseChanged?.Invoke();
        }

        private void LaunchedGogoObserver_OnExpanded()
        {
            Phase = Phase.Settling;
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

        private void PlayerGogoReturner_OnFinished()
        {
            Phase = Phase.Selecting;
            PhaseChanged?.Invoke();
        }
    }
}
