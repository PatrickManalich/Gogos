using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gogos
{
    public class AccelerometerSettlingWatcher : MonoBehaviour
    {
        public event Action Settled;

        private bool m_IsWatching;
        private List<Accelerometer> m_Accelerometers = new List<Accelerometer>();
        private List<Accelerometer> m_MovingAccelerometers = new List<Accelerometer>();

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void Update()
        {
            if (m_IsWatching && m_MovingAccelerometers.All(m => m == null || !m.IsMoving))
            {
                m_IsWatching = false;
                m_MovingAccelerometers.Clear();
                Settled?.Invoke();
            }
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Settling)
            {
                m_Accelerometers = FindObjectsOfType<Accelerometer>().ToList();
                m_MovingAccelerometers = m_Accelerometers.Where(m => m.IsMoving).ToList();
                m_IsWatching = true;
            }
        }
    }
}
