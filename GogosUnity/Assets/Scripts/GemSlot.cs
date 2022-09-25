using UnityEngine;

namespace Gogos
{
    public class GemSlot : MonoBehaviour
    {
        public Transform ReleaseParent { get; set; }

        public bool HasReleased => transform.parent == ReleaseParent;

        [SerializeField]
        private float m_BondStrength;

        private Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponentInChildren<Rigidbody>();
            m_Rigidbody.isKinematic = true;
        }

        public void ApplyForce(Vector3 force)
        {
            m_BondStrength -= force.magnitude;

            if (m_BondStrength <= 0)
            {
                m_Rigidbody.isKinematic = false;
                transform.parent = ReleaseParent;
            }
        }
    }
}
