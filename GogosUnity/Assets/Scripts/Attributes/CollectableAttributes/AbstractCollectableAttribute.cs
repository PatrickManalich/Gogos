using UnityEngine;

namespace Gogos
{
    public abstract class AbstractCollectableAttribute : MonoBehaviour
    {
        protected abstract void Collect();

        [SerializeField]
        private TriggerListener m_TriggerListener;

        [SerializeField]
        protected TierTrackerReference m_TierTrackerReference;

        private void Awake()
        {
            m_TriggerListener.Entered += TriggerListener_OnEntered;
        }

        private void OnDestroy()
        {
            m_TriggerListener.Entered -= TriggerListener_OnEntered;
        }

        private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
        {
            var collectTrigger = e.OtherCollider.GetComponent<CollectTrigger>();
            if (collectTrigger)
            {
                Collect();
                Destroy(m_TriggerListener.gameObject);
            }
        }
    }
}
