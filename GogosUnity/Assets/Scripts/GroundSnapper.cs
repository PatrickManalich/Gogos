using Gogos.Extensions;
using UnityEngine;

namespace Gogos
{
    public class GroundSnapper : MonoBehaviour
    {
        [SerializeField]
        private float m_VerticalOffset;

        private const string DefaultLayerName = "Default";
        private const float MaxRaycastDistance = 100;
        private const float RaycastOffset = 1;

        public void SnapToGround()
        {
            transform.localPosition = Vector3.zero;

            var defaultLayerMask = LayerMask.GetMask(DefaultLayerName);
            var origin = transform.position + new Vector3(0, RaycastOffset, 0);
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, MaxRaycastDistance + RaycastOffset, defaultLayerMask))
            {
                transform.rotation = Quaternion.Euler(hit.normal);
                transform.position = transform.position.WithY(hit.point.y + m_VerticalOffset);
            }
        }
    }
}
