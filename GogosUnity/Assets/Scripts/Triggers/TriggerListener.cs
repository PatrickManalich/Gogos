using System;
using UnityEngine;

namespace Gogos
{
    public class TriggerListener : MonoBehaviour
    {
        public event EventHandler<TriggerEventArgs> Entered;

        public event EventHandler<TriggerEventArgs> Exited;

        [SerializeField]
        private Collider m_Collider;

        private void OnTriggerEnter(Collider otherCollider)
        {
            Entered?.Invoke(this, new TriggerEventArgs(otherCollider));
        }

        private void OnTriggerExit(Collider otherCollider)
        {
            Exited?.Invoke(this, new TriggerEventArgs(otherCollider));
        }

        public void EnableTrigger()
        {
            m_Collider.enabled = true;
        }

        public void DisableTrigger()
        {
            m_Collider.enabled = false;
        }
    }
}
