using System;
using UnityEngine;

namespace Gogos
{
    public class BlastTriggerEventArgs : EventArgs
    {
        public BlastTrigger BlastTrigger { get; }

        public Vector3 Force { get; }

        public BlastTriggerEventArgs(BlastTrigger blastTrigger, Vector3 force)
        {
            BlastTrigger = blastTrigger;
            Force = force;
        }
    }
}
