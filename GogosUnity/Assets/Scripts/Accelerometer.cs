using System;
using UnityEngine;

namespace Gogos
{
    public class Accelerometer : MonoBehaviour
    {
        public event Action StartedMoving;

        public event Action StoppedMoving;

        public bool IsMoving { get; private set; }

        [SerializeField]
        private Rigidbody m_Rigidbody;

        private const float MovementThreshold = 1;
        private const float RestThreshold = 0.001f;

        private void FixedUpdate()
        {
            if (!IsMoving && m_Rigidbody.velocity.magnitude > MovementThreshold)
            {
                IsMoving = true;
                StartedMoving?.Invoke();
            }
            else if (IsMoving && m_Rigidbody.velocity.magnitude < RestThreshold)
            {
                IsMoving = false;
                StoppedMoving?.Invoke();
            }
        }

        private void OnDestroy()
        {
            if (IsMoving)
            {
                IsMoving = false;
                StoppedMoving?.Invoke();
            }
        }
    }
}
