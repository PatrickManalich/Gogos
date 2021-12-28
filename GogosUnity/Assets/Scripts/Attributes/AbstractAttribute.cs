using UnityEngine;

namespace Gogos
{
    public abstract class AbstractAttribute : MonoBehaviour
    {
        public Player Player { get; set; }

        protected abstract void OnTriggerEntered(TriggerEventArgs e);

        protected abstract void OnTriggerExited(TriggerEventArgs e);

        [SerializeField]
        protected TriggerListener m_TriggerListener;

        private void Awake()
        {
            m_TriggerListener.Entered += TriggerListener_OnEntered;
            m_TriggerListener.Exited += TriggerListener_OnExited;
        }

        private void OnDestroy()
        {
            m_TriggerListener.Exited -= TriggerListener_OnExited;
            m_TriggerListener.Entered -= TriggerListener_OnEntered;
        }

        private void TriggerListener_OnEntered(object sender, TriggerEventArgs e)
        {
            OnTriggerEntered(e);
        }

        private void TriggerListener_OnExited(object sender, TriggerEventArgs e)
        {
            OnTriggerExited(e);
        }
    }
}
