using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class AccelerometerSettlingWatcher : MonoBehaviour
    {
        public event Action Settled;

        private const float SettledTimeout = 1;

        private bool m_IsWatching;
        private List<Accelerometer> m_Accelerometers = new List<Accelerometer>();
        private int m_AccelerometersMoving;
        private float m_Timer;

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            ForgetAllAccelerometers();
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void Update()
        {
            if (m_IsWatching)
            {
                if (m_AccelerometersMoving == 0)
                {
                    m_Timer += Time.deltaTime;
                    if (m_Timer >= SettledTimeout)
                    {
                        m_IsWatching = false;
                        ForgetAllAccelerometers();
                        Settled?.Invoke();
                    }
                }
                else
                {
                    m_Timer = 0;
                }
            }
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Settling)
            {
                m_Timer = 0;
                WatchAllAccelerometers();
                m_IsWatching = true;
            }
        }

        private void Accelerometer_OnStartedMoving()
        {
            m_AccelerometersMoving++;
        }

        private void Accelerometer_OnStoppedMoving()
        {
            m_AccelerometersMoving--;
        }

        private void WatchAllAccelerometers()
        {
            m_Accelerometers = FindObjectsOfType<Accelerometer>().ToList();
            foreach (var accelerometer in m_Accelerometers)
            {
                accelerometer.StartedMoving += Accelerometer_OnStartedMoving;
                accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
                if (accelerometer.IsMoving)
                {
                    m_AccelerometersMoving++;
                }
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
            m_AccelerometersMoving = 0;
        }
    }
}
