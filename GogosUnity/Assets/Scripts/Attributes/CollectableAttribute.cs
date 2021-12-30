using System;

namespace Gogos
{
    public class CollectableAttribute : AbstractAttribute
    {
        public event Action Collected;

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var collectTrigger = e.OtherCollider.GetComponent<CollectTrigger>();
            if (collectTrigger != null)
            {
                Collected?.Invoke();
                Destroy(m_TriggerListener.gameObject);
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
