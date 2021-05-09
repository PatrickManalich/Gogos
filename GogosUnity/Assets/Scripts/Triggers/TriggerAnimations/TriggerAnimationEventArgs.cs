using System;

namespace Gogos
{
    public enum TriggerAnimation { Invisible, Expand, Expanded }

    public class TriggerAnimationEventArgs : EventArgs
    {
        public TriggerAnimation TriggerAnimation { get; }

        public TriggerAnimationEventArgs(TriggerAnimation triggerAnimation)
        {
            TriggerAnimation = triggerAnimation;
        }
    }
}