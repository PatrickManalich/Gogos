using System;
using UnityEngine;

namespace Gogos
{
    public class Accelerometer : MonoBehaviour
    {
        public event Action StartedMoving;
        public event Action StoppedMoving;

        [SerializeField]
        private Rigidbody m_Rigidbody;

        private const float MovementThreshold = 1;
        private const float RestThreshold = 0.001f;

        private bool m_Moving;

        private void FixedUpdate()
        {
            if (!m_Moving && m_Rigidbody.velocity.magnitude > MovementThreshold)
            {
                m_Moving = true;
                StartedMoving?.Invoke();
            }
            else if (m_Moving && m_Rigidbody.velocity.magnitude < RestThreshold)
            {
                m_Moving = false;
                StoppedMoving?.Invoke();
            }
        }
    }
}