using UnityEngine;

namespace Gogos
{
    public class SpawnMarker : MonoBehaviour
    {
        public void MarkWithRadius(float radius)
        {
            transform.localScale = 2 * radius * Vector3.one;
        }
    }
}
