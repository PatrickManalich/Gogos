using UnityEngine;

namespace Gogos
{
    public class RootColliderIgnorer : MonoBehaviour
    {
        [SerializeField]
        private Collider m_Collider;

        private void Awake()
        {
            var rootCollider = transform.GetComponentInParent<Rigidbody>()?.GetComponent<Collider>();
            if (rootCollider != null)
            {
                Physics.IgnoreCollision(m_Collider, rootCollider, true);
            }
        }
    }
}
