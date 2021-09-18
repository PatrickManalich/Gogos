using UnityEngine;

namespace Gogos
{
    public class RotationAligner : MonoBehaviour
    {
        public void AlignWithRotation(Quaternion rotation)
        {
            var oldParent = transform.parent;

            transform.localPosition = Vector3.zero;
            transform.parent = null;
            transform.rotation = rotation;
            transform.parent = oldParent;
        }
    }
}
