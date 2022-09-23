using UnityEngine;

namespace Gogos
{
    public class ChildRigidbodyConstrainer : MonoBehaviour
    {
        private Rigidbody[] m_ChildRigidbodies;

        private void Awake()
        {
            m_ChildRigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (var childRigidbody in m_ChildRigidbodies)
            {
                childRigidbody.isKinematic = true;
            }
        }

        public void Release()
        {
            foreach (var childRigidbody in m_ChildRigidbodies)
            {
                childRigidbody.isKinematic = false;
            }
        }
    }
}
