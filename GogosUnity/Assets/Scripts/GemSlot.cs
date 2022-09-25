using UnityEngine;

namespace Gogos
{
    public class GemSlot : MonoBehaviour
    {
        public Transform ReleaseParent { get; set; }

        private Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponentInChildren<Rigidbody>();
            m_Rigidbody.isKinematic = true;
        }

        public void Release()
        {
            m_Rigidbody.isKinematic = false;
            transform.parent = ReleaseParent;
        }
    }
}
