using UnityEngine;

namespace Gogos
{
    public class SupportTrigger : MonoBehaviour
    {
        public SupportAbility SupportAbility => m_SupportAbility;

        [SerializeField]
        private SupportAbility m_SupportAbility;
    }
}