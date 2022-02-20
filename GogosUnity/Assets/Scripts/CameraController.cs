using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera m_SpawningVirtualCamera;

        [SerializeField]
        private CinemachineVirtualCamera m_SelectingVirtualCamera;

        [SerializeField]
        private CinemachineVirtualCamera m_LaunchingVirtualCamera;

        [SerializeField]
        private CinemachineVirtualCamera m_SettlingVirtualCamera;

        [SerializeField]
        private VirtualCameraOrbiter m_SettlingVirtualCameraOrbiter;

        [SerializeField]
        private Launcher m_Launcher;

        [SerializeField]
        private GameObject m_ProjectilePoint;

        [SerializeField]
        private LaunchedGogoObserver m_LaunchedGogoObserver;

        private static readonly Dictionary<RangeTier, Vector3> OrbitOffsetsByRangeTier = new Dictionary<RangeTier, Vector3>()
        {
            { RangeTier.Disabled, new Vector3(25, 10, 25) },
            { RangeTier.Small, new Vector3(25, 10, 25) },
            { RangeTier.Medium, new Vector3(35, 15, 35) },
            { RangeTier.Large, new Vector3(45, 20, 45) },
        };

        private CinemachineComposer m_SelectingComposer;

        private void Awake()
        {
            m_SelectingComposer = m_SelectingVirtualCamera.GetCinemachineComponent<CinemachineComposer>();

            m_SpawningVirtualCamera.gameObject.SetActive(true);
            m_SelectingVirtualCamera.gameObject.SetActive(false);
            m_LaunchingVirtualCamera.gameObject.SetActive(false);
            m_SettlingVirtualCamera.gameObject.SetActive(false);
        }

        private void Start()
        {
            PhaseTracker.PhaseChanged += PhaseTracker_OnPhaseChanged;
            m_LaunchedGogoObserver.StartedExpanding += LaunchedGogoObserver_OnStartedExpanding;
        }

        private void OnDestroy()
        {
            m_LaunchedGogoObserver.StartedExpanding -= LaunchedGogoObserver_OnStartedExpanding;
            PhaseTracker.PhaseChanged -= PhaseTracker_OnPhaseChanged;
        }

        private void Update()
        {
            if (PhaseTracker.Phase == Phase.Selecting)
            {
                if (m_Launcher.IsTurning || m_Launcher.IsPowering)
                {
                    m_SelectingComposer.m_DeadZoneWidth = Mathf.Max(m_SelectingComposer.m_DeadZoneWidth - Time.deltaTime, 0);
                    m_SelectingComposer.m_DeadZoneHeight = Mathf.Max(m_SelectingComposer.m_DeadZoneHeight - Time.deltaTime, 0);
                    m_SelectingComposer.m_SoftZoneWidth = Mathf.Max(m_SelectingComposer.m_SoftZoneWidth - Time.deltaTime, 0.3f);
                    m_SelectingComposer.m_SoftZoneHeight = Mathf.Max(m_SelectingComposer.m_SoftZoneHeight - Time.deltaTime, 0.3f);
                }
                else
                {
                    m_SelectingComposer.m_DeadZoneWidth = 0.25f;
                    m_SelectingComposer.m_DeadZoneHeight = 0.25f;
                    m_SelectingComposer.m_SoftZoneWidth = 0.5f;
                    m_SelectingComposer.m_SoftZoneHeight = 0.5f;
                }
            }
        }

        private void FixedUpdate()
        {
            if (PhaseTracker.Phase == Phase.Launching || PhaseTracker.Phase == Phase.Settling)
            {
                if (m_Launcher.ProjectileRigidbody != null)
                {
                    m_ProjectilePoint.transform.position = m_Launcher.ProjectileRigidbody.transform.position;
                    m_ProjectilePoint.transform.rotation = Quaternion.Euler(new Vector3(0, m_Launcher.transform.rotation.eulerAngles.y, 0));
                }
            }
        }

        private void PhaseTracker_OnPhaseChanged()
        {
            if (PhaseTracker.Phase == Phase.Spawning)
            {
                m_SpawningVirtualCamera.gameObject.SetActive(true);
                m_SettlingVirtualCamera.gameObject.SetActive(false);
            }
            else if (PhaseTracker.Phase == Phase.Selecting)
            {
                m_SelectingVirtualCamera.gameObject.SetActive(true);
                m_SpawningVirtualCamera.gameObject.SetActive(false);
            }
            else if (PhaseTracker.Phase == Phase.Launching)
            {
                m_LaunchingVirtualCamera.transform.position = m_SelectingVirtualCamera.transform.position;
                m_LaunchingVirtualCamera.transform.rotation = m_SelectingVirtualCamera.transform.rotation;
                m_LaunchingVirtualCamera.gameObject.SetActive(true);
                m_SelectingVirtualCamera.gameObject.SetActive(false);
            }
        }

        private void LaunchedGogoObserver_OnStartedExpanding()
        {
            var rangeTierTracker = (RangeTierTracker)m_LaunchedGogoObserver.LaunchedGogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            m_SettlingVirtualCameraOrbiter.SetOrbitOffset(OrbitOffsetsByRangeTier[rangeTierTracker.Tier]);
            m_SettlingVirtualCamera.gameObject.SetActive(true);
            m_LaunchingVirtualCamera.gameObject.SetActive(false);
        }
    }
}
