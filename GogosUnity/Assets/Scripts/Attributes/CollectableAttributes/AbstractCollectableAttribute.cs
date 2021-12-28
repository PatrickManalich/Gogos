namespace Gogos
{
    public abstract class AbstractCollectableAttribute : AbstractAttribute
    {
        protected abstract void Collect();

        protected override void OnTriggerEntered(TriggerEventArgs e)
        {
            var collectTrigger = e.OtherCollider.GetComponent<CollectTrigger>();
            if (collectTrigger != null)
            {
                Collect();
                Destroy(m_TriggerListener.gameObject);
            }
        }

        protected override void OnTriggerExited(TriggerEventArgs e)
        { }
    }
}
