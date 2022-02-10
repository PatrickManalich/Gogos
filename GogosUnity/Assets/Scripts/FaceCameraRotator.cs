using UnityEngine;

namespace Gogos
{
    public class FaceCameraRotator : MonoBehaviour
    {
        private void Start()
        {
            transform.LookAt(Camera.main.transform);
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
