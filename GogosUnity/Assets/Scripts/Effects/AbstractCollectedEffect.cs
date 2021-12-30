using UnityEngine;

namespace Gogos
{
    public abstract class AbstractCollectedEffect : MonoBehaviour
    {
        protected abstract void OnCollected();

        [SerializeField]
        private CollectableAttribute m_CollectableAttribute;

        private void Start()
        {
            m_CollectableAttribute.Collected += CollectableAttribute_OnCollected;
        }

        private void OnDestroy()
        {
            m_CollectableAttribute.Collected -= CollectableAttribute_OnCollected;
        }

        private void CollectableAttribute_OnCollected()
        {
            OnCollected();
        }
    }
}
