﻿using Cinemachine;
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
        private Launcher m_Launcher;

        [SerializeField]
        private GameObject m_HitPoint;

        [SerializeField]
        private GameObject m_ProjectilePoint;

        [SerializeField]
        private LaunchedGogoObserver m_LaunchedGogoObserver;

        private CinemachineComposer m_SelectingComposer;
        private Vector3 m_LastHitPointPosition;

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
                var isHitPointMoving = m_HitPoint.transform.position != m_LastHitPointPosition;
                m_SelectingComposer.m_DeadZoneWidth = isHitPointMoving ? 0 : 0.5f;
                m_SelectingComposer.m_DeadZoneHeight = isHitPointMoving ? 0 : 0.5f;
                m_SelectingComposer.m_SoftZoneWidth = isHitPointMoving ? 0.3f : 1;
                m_SelectingComposer.m_SoftZoneHeight = isHitPointMoving ? 0.3f : 1;

                m_LastHitPointPosition = m_HitPoint.transform.position;
            }
            else if (PhaseTracker.Phase == Phase.Launching || PhaseTracker.Phase == Phase.Settling)
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
                m_LaunchingVirtualCamera.gameObject.SetActive(true);
                m_SelectingVirtualCamera.gameObject.SetActive(false);
            }
        }

        private void LaunchedGogoObserver_OnStartedExpanding()
        {
            m_SettlingVirtualCamera.gameObject.SetActive(true);
            m_LaunchingVirtualCamera.gameObject.SetActive(false);
        }
    }
}
