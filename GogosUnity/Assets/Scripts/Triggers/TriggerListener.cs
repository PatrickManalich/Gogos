using System;
using UnityEngine;

namespace Gogos
{
    public class TriggerListener : MonoBehaviour
    {
        public event EventHandler<TriggerEventArgs> Entered;
        public event EventHandler<TriggerEventArgs> Exited;

        [SerializeField]
        private bool m_IgnoreRootCollider;

        private Collider m_Collider;

        private void Awake()
        {
            m_Collider = GetComponent<Collider>();

            if (m_IgnoreRootCollider)
            {
                var rootCollider = transform.GetComponentInParent<Rigidbody>().GetComponent<Collider>();
                Physics.IgnoreCollision(m_Collider, rootCollider, true);
            }
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            Entered?.Invoke(this, new TriggerEventArgs(otherCollider));
        }

        private void OnTriggerExit(Collider otherCollider)
        {
            Exited?.Invoke(this, new TriggerEventArgs(otherCollider));
        }
    }
}