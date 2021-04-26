using System;
using UnityEngine;

namespace Gogos
{
    public class TriggerEventArgs : EventArgs
    {
        public Collider OtherCollider { get; }

        public TriggerEventArgs(Collider otherCollider)
        {
            OtherCollider = otherCollider;
        }
    }
}