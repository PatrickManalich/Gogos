using UnityEngine;

namespace Gogos
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField]
        private GroundSnapper m_GroundSnapper;

        public void MarkWithRadius(float radius)
        {
            m_GroundSnapper.SnapToGround();
            transform.localScale = 2 * radius * Vector3.one;
        }
    }
}
