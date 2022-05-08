﻿using System;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public enum Phase { Spawning, Transitioning, Returning, Selecting, Launching, Settling }

    public class PhaseTracker : AbstractSingleton<PhaseTracker>
    {
        public static event Action PhaseChanged;

        public static Phase Phase { get; private set; }

        [SerializeField]
        private SpawnerRandomizer m_SpawnerRandomizer;

        [SerializeField]
        private PlayerTransitioner m_PlayerTransitioner;

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
            Phase = Phase.Transitioning;
        }

        private void Start()
        {
            m_SpawnerRandomizer.Spawned += SpawnerRandomizer_OnFinished;
            m_SpawnerRandomizer.Skipped += SpawnerRandomizer_OnFinished;
            m_PlayerTransitioner.Transitioned += PlayerTransitioner_OnTransitioned;
            m_PlayerGogoReturner.Returned += PlayerGogoReturner_OnFinished;
            m_PlayerGogoReturner.Skipped += PlayerGogoReturner_OnFinished;
            m_Launcher.Launched += Launcher_OnLaunched;
            m_LaunchedGogoObserver.Expanded += LaunchedGogoObserver_OnFinished;
            m_LaunchedGogoObserver.Collected += LaunchedGogoObserver_OnFinished;
            m_AccelerometerSettlingWatcher.Settled += AccelerometerSettlingWatcher_OnSettled;
        }

        private void OnDestroy()
        {
            m_AccelerometerSettlingWatcher.Settled -= AccelerometerSettlingWatcher_OnSettled;
            m_LaunchedGogoObserver.Collected -= LaunchedGogoObserver_OnFinished;
            m_LaunchedGogoObserver.Expanded -= LaunchedGogoObserver_OnFinished;
            m_Launcher.Launched -= Launcher_OnLaunched;
            m_PlayerGogoReturner.Skipped -= PlayerGogoReturner_OnFinished;
            m_PlayerGogoReturner.Returned -= PlayerGogoReturner_OnFinished;
            m_PlayerTransitioner.Transitioned -= PlayerTransitioner_OnTransitioned;
            m_SpawnerRandomizer.Skipped -= SpawnerRandomizer_OnFinished;
            m_SpawnerRandomizer.Spawned -= SpawnerRandomizer_OnFinished;
        }

        private void SpawnerRandomizer_OnFinished()
        {
            ChangePhase(Phase.Transitioning);
        }

        private void PlayerTransitioner_OnTransitioned()
        {
            ChangePhase(Phase.Returning);
        }

        private void PlayerGogoReturner_OnFinished()
        {
            ChangePhase(Phase.Selecting);
        }

        private void Launcher_OnLaunched()
        {
            ChangePhase(Phase.Launching);
        }

        private void LaunchedGogoObserver_OnFinished()
        {
            ChangePhase(Phase.Settling);
        }

        private void AccelerometerSettlingWatcher_OnSettled()
        {
            ChangePhase(Phase.Spawning);
        }

        private void ChangePhase(Phase phase)
        {
            StartCoroutine(ChangePhaseRoutine(phase));
        }

        private IEnumerator ChangePhaseRoutine(Phase phase)
        {
            yield return null;  // Wait a frame to ensure other scripts have finished processing events
            Phase = phase;
            PhaseChanged?.Invoke();
        }
    }
}
