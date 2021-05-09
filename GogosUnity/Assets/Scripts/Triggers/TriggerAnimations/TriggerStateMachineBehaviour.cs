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

        private int m_AnimationLoopCount;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_AnimationLoopCount = 0;
            AnimationStarted?.Invoke(this, new TriggerAnimationEventArgs(m_TriggerAnimation));
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime - m_AnimationLoopCount >= 1)
            {
                m_AnimationLoopCount++;
                AnimationFinished?.Invoke(this, new TriggerAnimationEventArgs(m_TriggerAnimation));
            }
        }
    }
}