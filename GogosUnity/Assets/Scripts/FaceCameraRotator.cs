using UnityEngine;

namespace Gogos
{
    public class FaceCameraRotator : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
