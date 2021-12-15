using UnityEngine;

namespace Gogos
{
    public class SupportedVisual : MonoBehaviour
    {
        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private SupportableAttribute m_SupportableAttribute;

        [SerializeField]
        private GameObject m_Visual;

        private void Update()
        {
            m_Visual.SetActive(m_SupportableAttribute.IsSupported && !m_Accelerometer.IsMoving);
        }
    }
}
