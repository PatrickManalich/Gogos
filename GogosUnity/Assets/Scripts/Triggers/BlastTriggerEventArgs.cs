using System;

namespace Gogos
{
    public class BlastTriggerEventArgs : EventArgs
    {
        public BlastTrigger BlastTrigger { get; }

        public BlastTriggerEventArgs(BlastTrigger blastTrigger)
        {
            BlastTrigger = blastTrigger;
        }
    }
}
