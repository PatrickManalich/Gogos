using Gogos.Extensions;
using Gogos.Managers;
using System;
using UnityEngine;

namespace Gogos
{
	public class GogoLauncher : MonoBehaviour
	{
        public event Action LaunchPrepared;
        public event Action Launched;

        public Vector3 LaunchPoint => transform.position;
        public Vector3 LaunchVector => transform.forward * m_CurrentLaunchForce;

        [SerializeField]
        private GameObject m_ForcePoint;

        [SerializeField]
        private float m_LaunchAngle;

        [SerializeField]
        private float m_MinLaunchForce;

        [SerializeField]
        private float m_MaxLaunchForce;

        [SerializeField]
        private float m_LaunchForceDelta;

        [SerializeField]
        private Rigidbody m_GogoRigidbody;

        [SerializeField]
        private GameObject m_Target;

        [SerializeField]
        private float m_MovementSpeed;

        private float m_CurrentLaunchForce;
        private float m_DistanceToTarget;
        private float m_MovementAngle;
        private bool m_ReadyForLaunch;

        private void Start()
		{
            m_CurrentLaunchForce = (m_MinLaunchForce + m_MaxLaunchForce) / 2;
            m_DistanceToTarget = Vector3.Distance(transform.position, m_Target.transform.position);
            m_MovementAngle = 270;
            AlignLauncher();
            PrepareForLaunch();
        }

        private void Update()
        {
            if (InputManager.LaunchGogoKeyDown && m_ReadyForLaunch)
            {
                Launch();
            }
            if (InputManager.ResetGogoKeyDown)
            {
                PrepareForLaunch();
            }
            if (InputManager.MoveLauncherLeftKey)
            {
                m_MovementAngle -= Time.deltaTime * m_MovementSpeed;
                m_MovementAngle = m_MovementAngle.ClampAngle();
                AlignLauncher();
            }
            if (InputManager.MoveLauncherRightKey)
            {
                m_MovementAngle += Time.deltaTime * m_MovementSpeed;
                m_MovementAngle = m_MovementAngle.ClampAngle();
                AlignLauncher();
            }
            if (InputManager.IncreaseLaunchForceKey)
            {
                m_CurrentLaunchForce += Time.deltaTime * m_LaunchForceDelta;
                m_CurrentLaunchForce = Mathf.Clamp(m_CurrentLaunchForce, m_MinLaunchForce, m_MaxLaunchForce);
            }
            if (InputManager.DecreaseLaunchForceKey)
            {
                m_CurrentLaunchForce -= Time.deltaTime * m_LaunchForceDelta;
                m_CurrentLaunchForce = Mathf.Clamp(m_CurrentLaunchForce, m_MinLaunchForce, m_MaxLaunchForce);
            }
        }

        private void Launch()
        {
            m_GogoRigidbody.transform.parent = null;
            m_GogoRigidbody.isKinematic = false;
            m_GogoRigidbody.AddForceAtPosition(LaunchVector * m_GogoRigidbody.mass, m_ForcePoint.transform.position, ForceMode.Impulse);
            m_ReadyForLaunch = false;
            Launched?.Invoke();
        }

        private void PrepareForLaunch()
        {
            m_GogoRigidbody.isKinematic = true;
            m_GogoRigidbody.transform.parent = transform;
            m_GogoRigidbody.transform.localPosition = Vector3.zero;
            m_GogoRigidbody.transform.localRotation = Quaternion.identity;
            m_ReadyForLaunch = true;
            LaunchPrepared?.Invoke();
        }

        private void AlignLauncher()
        {
            var x = Mathf.Cos(m_MovementAngle.DegreesToRadians()) * m_DistanceToTarget;
            var y = 0;
            var z = Mathf.Sin(m_MovementAngle.DegreesToRadians()) * m_DistanceToTarget;
            transform.position = new Vector3(x, y, z);
            transform.LookAt(m_Target.transform);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithX(m_LaunchAngle));
        }
    }
}