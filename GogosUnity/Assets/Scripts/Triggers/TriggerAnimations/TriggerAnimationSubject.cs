using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class TriggerAnimationSubject : MonoBehaviour
    {
        [SerializeField]
        private Animator m_TriggerAnimator;

        private TriggerStateMachineBehaviour[] m_TriggerStateMachineBehaviours;
        private Dictionary<TriggerAnimation, List<ITriggerAnimationObserver>> m_ObserversByStartedTriggerAnimation = new Dictionary<TriggerAnimation, List<ITriggerAnimationObserver>>();
        private Dictionary<TriggerAnimation, List<ITriggerAnimationObserver>> m_ObserversByFinishedTriggerAnimation = new Dictionary<TriggerAnimation, List<ITriggerAnimationObserver>>();

        private void Awake()
        {
            m_TriggerStateMachineBehaviours = m_TriggerAnimator.GetBehaviours<TriggerStateMachineBehaviour>();
            foreach (var triggerStateMachineBehaviour in m_TriggerStateMachineBehaviours)
            {
                var triggerAnimation = triggerStateMachineBehaviour.TriggerAnimation;
                m_ObserversByStartedTriggerAnimation[triggerAnimation] = new List<ITriggerAnimationObserver>();
                m_ObserversByFinishedTriggerAnimation[triggerAnimation] = new List<ITriggerAnimationObserver>();

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

        public void AddObserverForAnimationStarted(ITriggerAnimationObserver triggerAnimationObserver, TriggerAnimation triggerAnimation)
        {
            m_ObserversByStartedTriggerAnimation[triggerAnimation].Add(triggerAnimationObserver);
        }

        public void AddObserverForAnimationFinished(ITriggerAnimationObserver triggerAnimationObserver, TriggerAnimation triggerAnimation)
        {
            m_ObserversByFinishedTriggerAnimation[triggerAnimation].Add(triggerAnimationObserver);
        }

        public void RemoveObserverForAnimationStarted(ITriggerAnimationObserver triggerAnimationObserver, TriggerAnimation triggerAnimation)
        {
            m_ObserversByStartedTriggerAnimation[triggerAnimation].Remove(triggerAnimationObserver);
        }

        public void RemoveObserverForAnimationFinished(ITriggerAnimationObserver triggerAnimationObserver, TriggerAnimation triggerAnimation)
        {
            m_ObserversByFinishedTriggerAnimation[triggerAnimation].Remove(triggerAnimationObserver);
        }

        private void TriggerStateMachineBehaviour_OnAnimationStarted(object sender, TriggerAnimationEventArgs e)
        {
            var observers = m_ObserversByStartedTriggerAnimation[e.TriggerAnimation];
            NotifyObservers(observers);
        }

        private void TriggerStateMachineBehaviour_OnAnimationFinished(object sender, TriggerAnimationEventArgs e)
        {
            var observers = m_ObserversByFinishedTriggerAnimation[e.TriggerAnimation];
            NotifyObservers(observers);
        }

        private void NotifyObservers(List<ITriggerAnimationObserver> observers)
        {
            // Clone list in case observers remove themselves when notifying, which would modify
            // the list while it's still being iterated upon

            var clonedObservers = new List<ITriggerAnimationObserver>(observers);
            foreach (var clonedObserver in clonedObservers)
            {
                clonedObserver.Notify();
            }
        }
    }
}
