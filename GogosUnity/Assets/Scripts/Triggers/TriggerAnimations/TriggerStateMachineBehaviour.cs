using System;
using UnityEngine;

namespace Gogos
{
    public class TriggerStateMachineBehaviour : StateMachineBehaviour
    {
        public event EventHandler<TriggerAnimationEventArgs> AnimationStarted;

        public event EventHandler<TriggerAnimationEventArgs> AnimationFinished;

        public TriggerAnimation TriggerAnimation => m_TriggerAnimation;

        [SerializeField]
        private TriggerAnimation m_TriggerAnimation = default;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AnimationStarted?.Invoke(this, new TriggerAnimationEventArgs(m_TriggerAnimation));
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AnimationFinished?.Invoke(this, new TriggerAnimationEventArgs(m_TriggerAnimation));
        }
    }
}
