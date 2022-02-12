using UnityEngine;

namespace Gogos
{
    public abstract class AbstractBlastedEffect : MonoBehaviour
    {
        protected abstract void OnBlasted(BlastTriggerEventArgs e);

        [SerializeField]
        private BlastableAttribute m_BlastableAttribute;

        protected virtual void Start()
        {
            m_BlastableAttribute.Blasted += BlastableAttribute_OnBlasted;
        }

        protected virtual void OnDestroy()
        {
            m_BlastableAttribute.Blasted -= BlastableAttribute_OnBlasted;
        }

        private void BlastableAttribute_OnBlasted(object sender, BlastTriggerEventArgs e)
        {
            OnBlasted(e);
        }
    }
}
