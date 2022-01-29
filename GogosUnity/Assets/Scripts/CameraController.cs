using Cinemachine;
using UnityEngine;

namespace Gogos
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private CinemachineVirtualCamera m_SpawningVirtualCamera;

        [SerializeField]
        private CinemachineVirtualCamera m_SelectingVirtualCamera;

        private CinemachineTransposer m_SelectingTransposer;
        private CinemachineComposer m_SelectingComposer;
        private Vector3 m_LastLauncherPosition;
        private Quaternion m_LastLauncherRotation;

        private void Awake()
        {
            m_SelectingTransposer = m_SelectingVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            m_SelectingComposer = m_SelectingVirtualCamera.GetCinemachineComponent<CinemachineComposer>();

            m_SpawningVirtualCamera.gameObject.SetActive(true);
            m_SelectingVirtualCamera.gameObject.SetActive(false);
        }

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
        }

        private void OnDestroy()
        {
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void Update()
        {
            if (PhaseTracker.Phase == Phase.Selecting)
            {
                var isLauncherTurning = m_Launcher.transform.position == m_LastLauncherPosition && m_Launcher.transform.rotation != m_LastLauncherRotation;
                if (isLauncherTurning)
                {
                    m_SelectingTransposer.m_XDamping = 0;
                    m_SelectingTransposer.m_YDamping = 0;
                    m_SelectingTransposer.m_ZDamping = 0;
                    m_SelectingTransposer.m_YawDamping = 0;
                    m_SelectingComposer.m_SoftZoneWidth = 0.05f;
                    m_SelectingComposer.m_SoftZoneHeight = 0.05f;
                }
                else
                {
                    m_SelectingTransposer.m_XDamping = 2;
                    m_SelectingTransposer.m_YDamping = 2;
                    m_SelectingTransposer.m_ZDamping = 2;
                    m_SelectingTransposer.m_YawDamping = 2;
                    m_SelectingComposer.m_SoftZoneWidth = 0.5f;
                    m_SelectingComposer.m_SoftZoneHeight = 0.5f;
                }

                m_LastLauncherPosition = m_Launcher.transform.position;
                m_LastLauncherRotation = m_Launcher.transform.rotation;
            }
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Spawning)
            {
                m_SpawningVirtualCamera.gameObject.SetActive(true);
                m_SelectingVirtualCamera.gameObject.SetActive(false);
            }
            else if (PhaseTracker.Phase == Phase.Selecting)
            {
                m_SelectingVirtualCamera.gameObject.SetActive(true);
                m_SpawningVirtualCamera.gameObject.SetActive(false);
                m_LastLauncherPosition = m_Launcher.transform.position;
                m_LastLauncherRotation = m_Launcher.transform.rotation;
            }
        }
    }
}
