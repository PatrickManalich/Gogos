﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public enum Phase { Selecting, Launching, Settling }

    public class PhaseTracker : MonoBehaviour, ITriggerAnimationObserver
    {
        public event Action PhaseChanged;

        public Phase Phase { get; private set; }

        [SerializeField]
        private Launcher m_Launcher;

        private List<Accelerometer> m_Accelerometers = new List<Accelerometer>();
        private int m_AccelerometersMoving;
        private TriggerAnimationSubject m_TriggerAnimationSubject;

        private void Start()
        {
            m_Launcher.Launched += Launcher_OnLaunched;
        }

        private void OnDestroy()
        {
            ForgetAllAccelerometers();
            m_Launcher.Launched -= Launcher_OnLaunched;
        }

        private void Update()
        {
            if (Phase == Phase.Settling)
            {
                var hasEverythingSettled = m_AccelerometersMoving == 0;
                if (hasEverythingSettled)
                {
                    Phase = Phase.Selecting;
                    PhaseChanged?.Invoke();
                }
            }
        }

        public void Notify()
        {
            m_TriggerAnimationSubject.RemoveObserverForAnimationFinished(this, TriggerAnimation.Expand);
            m_TriggerAnimationSubject = null;

            Phase = Phase.Settling;
            PhaseChanged?.Invoke();
        }

        private void Accelerometer_OnStartedMoving()
        {
            m_AccelerometersMoving++;
        }

        private void Accelerometer_OnStoppedMoving()
        {
            m_AccelerometersMoving--;
        }

        private void Launcher_OnLaunched()
        {
            ForgetAllAccelerometers();
            WatchAllAccelerometers();

            m_TriggerAnimationSubject = m_Launcher.Projectile.GetComponentInChildren<TriggerAnimationSubject>();
            m_TriggerAnimationSubject.AddObserverForAnimationFinished(this, TriggerAnimation.Expand);

            Phase = Phase.Launching;
            PhaseChanged?.Invoke();
        }

        private void WatchAllAccelerometers()
        {
            m_Accelerometers = FindObjectsOfType<Accelerometer>().ToList();
            foreach (var accelerometer in m_Accelerometers)
            {
                accelerometer.StartedMoving += Accelerometer_OnStartedMoving;
                accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
            }
        }

        private void ForgetAllAccelerometers()
        {
            foreach (var accelerometer in m_Accelerometers)
            {
                accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
                accelerometer.StartedMoving -= Accelerometer_OnStartedMoving;
            }
            m_Accelerometers.Clear();
        }
    }
}
