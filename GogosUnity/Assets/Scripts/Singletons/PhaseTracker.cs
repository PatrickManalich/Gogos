using System;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public enum Phase { Spawning, Returning, Selecting, Launching, Settling, Transitioning }

    public class PhaseTracker : AbstractSingleton<PhaseTracker>
    {
        public static event Action PhaseChanged;

        public static Phase Phase { get; private set; }

        [SerializeField]
        private SpawnerRandomizer m_SpawnerRandomizer;

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
            Phase = Phase.Spawning;
        }

        private void Start()
        {
            m_SpawnerRandomizer.Spawned += SpawnerRandomizer_OnFinished;
            m_SpawnerRandomizer.Skipped += SpawnerRandomizer_OnFinished;
            m_PlayerGogoReturner.Returned += PlayerGogoReturner_OnFinished;
            m_PlayerGogoReturner.Skipped += PlayerGogoReturner_OnFinished;
            m_Launcher.Launched += Launcher_OnLaunched;
            m_LaunchedGogoObserver.Expanded += LaunchedGogoObserver_OnExpanded;
            m_AccelerometerSettlingWatcher.Settled += AccelerometerSettlingWatcher_OnSettled;
            PlayerTracker.PlayerChanged += PlayerTracker_OnPlayerChanged;
        }

        private void OnDestroy()
        {
            PlayerTracker.PlayerChanged -= PlayerTracker_OnPlayerChanged;
            m_AccelerometerSettlingWatcher.Settled -= AccelerometerSettlingWatcher_OnSettled;
            m_LaunchedGogoObserver.Expanded -= LaunchedGogoObserver_OnExpanded;
            m_Launcher.Launched -= Launcher_OnLaunched;
            m_PlayerGogoReturner.Skipped -= PlayerGogoReturner_OnFinished;
            m_PlayerGogoReturner.Returned -= PlayerGogoReturner_OnFinished;
            m_SpawnerRandomizer.Skipped -= SpawnerRandomizer_OnFinished;
            m_SpawnerRandomizer.Spawned -= SpawnerRandomizer_OnFinished;
        }

        private void SpawnerRandomizer_OnFinished()
        {
            Phase = Phase.Returning;
            PhaseChanged?.Invoke();
        }

        private void PlayerGogoReturner_OnFinished()
        {
            Phase = Phase.Selecting;
            PhaseChanged?.Invoke();
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
            Phase = Phase.Spawning;
            PhaseChanged?.Invoke();
        }
    }
}
