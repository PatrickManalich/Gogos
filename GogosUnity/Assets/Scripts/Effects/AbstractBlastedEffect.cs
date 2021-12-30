using UnityEngine;

namespace Gogos
{
    public abstract class AbstractBlastedEffect : MonoBehaviour
    {
        protected abstract void OnBlasted();

        [SerializeField]
        private BlastableAttribute m_BlastableAttribute;

        private void Start()
        {
            m_BlastableAttribute.Blasted += BlastableAttribute_OnBlasted;
        }

        private void OnDestroy()
        {
            m_BlastableAttribute.Blasted -= BlastableAttribute_OnBlasted;
        }

        private void BlastableAttribute_OnBlasted()
        {
            OnBlasted();
        }
    }
}
