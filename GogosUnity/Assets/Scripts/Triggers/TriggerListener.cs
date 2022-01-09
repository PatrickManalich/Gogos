using System;
using UnityEngine;

namespace Gogos
{
    public class TriggerListener : MonoBehaviour
    {
        public event EventHandler<TriggerEventArgs> Entered;

        public event EventHandler<TriggerEventArgs> Exited;

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
