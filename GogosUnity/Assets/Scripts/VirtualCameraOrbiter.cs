using Cinemachine;
using UnityEngine;

namespace Gogos
{
    public class VirtualCameraOrbiter : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera m_VirtualCamera;

        [SerializeField]
        private float m_StartAngle;

        [SerializeField]
        private Vector3 m_OrbitOffset;

        [SerializeField]
        private float m_MouseOrbitSpeed;

        private float m_Angle;
        private CinemachineTransposer m_Transposer;
        private CinemachineComposer m_Composer;
        private float m_PreviousDeadZoneWidth;
        private float m_PreviousDeadZoneHeight;
        private float m_PreviousSoftZoneWidth;
        private float m_PreviousSoftZoneHeight;

        private void Awake()
        {
            m_Transposer = m_VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            m_Composer = m_VirtualCamera.GetCinemachineComponent<CinemachineComposer>();
        }

        private void OnEnable()
        {
            m_Angle = m_StartAngle;
            AlignWithAngle();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_PreviousDeadZoneWidth = m_Composer.m_DeadZoneWidth;
                m_PreviousDeadZoneHeight = m_Composer.m_DeadZoneHeight;
                m_PreviousSoftZoneWidth = m_Composer.m_SoftZoneWidth;
                m_PreviousSoftZoneHeight = m_Composer.m_SoftZoneHeight;
            }
            if (Input.GetMouseButton(1))
            {
                m_Composer.m_DeadZoneWidth = 0;
                m_Composer.m_DeadZoneHeight = 0;
                m_Composer.m_SoftZoneWidth = 0;
                m_Composer.m_SoftZoneHeight = 0;

                m_Angle += Input.GetAxis("Mouse X") * m_MouseOrbitSpeed;
                AlignWithAngle();
            }
            if (Input.GetMouseButtonUp(1))
            {
                m_Composer.m_DeadZoneWidth = m_PreviousDeadZoneWidth;
                m_Composer.m_DeadZoneHeight = m_PreviousDeadZoneHeight;
                m_Composer.m_SoftZoneWidth = m_PreviousSoftZoneWidth;
                m_Composer.m_SoftZoneHeight = m_PreviousSoftZoneHeight;
            }
        }

        public void SetOrbitOffset(Vector3 orbitOffset)
        {
            m_OrbitOffset = orbitOffset;
            AlignWithAngle();
        }

        private void AlignWithAngle()
        {
            var x = m_OrbitOffset.x * Mathf.Cos(m_Angle);
            var y = m_OrbitOffset.y;
            var z = m_OrbitOffset.z * Mathf.Sin(m_Angle);
            m_Transposer.m_FollowOffset = new Vector3(x, y, z);
        }
    }
}
