using System;
using UnityEngine;

namespace Gogos
{
    public class TriggerAnimationsReference : MonoBehaviour
    {
        public event EventHandler<TriggerAnimationEventArgs> AnimationStarted;

        public event EventHandler<TriggerAnimationEventArgs> AnimationFinished;

        [SerializeField]
        private Animator m_TriggerAnimator;

        private TriggerStateMachineBehaviour[] m_TriggerStateMachineBehaviours;

        private void Awake()
        {
            m_TriggerStateMachineBehaviours = m_TriggerAnimator.GetBehaviours<TriggerStateMachineBehaviour>();
            foreach (var triggerStateMachineBehaviour in m_TriggerStateMachineBehaviours)
            {
                triggerStateMachineBehaviour.AnimationStarted += TriggerStateMachineBehaviour_OnAnimationStarted;
                triggerStateMachineBehaviour.AnimationFinished += TriggerStateMachineBehaviour_OnAnimationFinished;
            }
        }

        private void OnDestroy()
        {
            foreach (var triggerStateMachineBehaviour in m_TriggerStateMachineBehaviours)
            {
                triggerStateMachineBehaviour.AnimationFinished -= TriggerStateMachineBehaviour_OnAnimationFinished;
                triggerStateMachineBehaviour.AnimationStarted -= TriggerStateMachineBehaviour_OnAnimationStarted;
            }
        }

        private void TriggerStateMachineBehaviour_OnAnimationStarted(object sender, TriggerAnimationEventArgs e)
        {
            AnimationStarted?.Invoke(this, new TriggerAnimationEventArgs(e.TriggerAnimation));
        }

        private void TriggerStateMachineBehaviour_OnAnimationFinished(object sender, TriggerAnimationEventArgs e)
        {
            AnimationFinished?.Invoke(this, new TriggerAnimationEventArgs(e.TriggerAnimation));
        }
    }
}
