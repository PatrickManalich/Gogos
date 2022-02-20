using Gogos.Extensions;
using System;
using System.Collections;
using UnityEngine;

namespace Gogos
{
    public class Launcher : MonoBehaviour
    {
        public event Action LaunchPrepared;

        public event Action Launched;

        public Vector3 LaunchPoint => transform.position;

        public Vector3 LaunchForce => transform.forward * m_LaunchPower;

        public Rigidbody ProjectileRigidbody => m_ProjectileRigidbody;

        public bool IsTurning { get; private set; }

        public bool IsPowering { get; private set; }

        public bool ReadyForLaunch { get; private set; }

        [SerializeField]
        private GameObject m_ForcePoint;

        [SerializeField]
        private LaunchPointTracker m_LaunchPointTracker;

        [SerializeField]
        private float m_VerticalOffset;

        [SerializeField]
        private float m_LaunchAngle;

        [SerializeField]
        private float m_MinLaunchPower;

        [SerializeField]
        private float m_MaxLaunchPower;

        [SerializeField]
        private float m_LaunchPowerDelta;

        [SerializeField]
        private float m_TurnSpeed;

        private float m_LaunchPower;
        private Rigidbody m_ProjectileRigidbody;
        private Coroutine m_StopTurningCoroutine;
        private Coroutine m_StopPoweringCoroutine;

        private void OnEnable()
        {
            if (m_LaunchPointTracker.LaunchPoints.Count > 0)
            {
                CycleLaunchPoint(0);
            }
        }

        public void LoadProjectile(GameObject projectile)
        {
            m_ProjectileRigidbody = projectile.GetComponent<Rigidbody>();
            PrepareForLaunch();
        }

        public void TurnLeft() => Turn(-1);

        public void TurnRight() => Turn(1);

        public void DecreaseLaunchPower() => ChangeLaunchPower(-1);

        public void IncreaseLaunchPower() => ChangeLaunchPower(1);

        public void CyclePreviousLaunchPoint() => CycleLaunchPoint(-1);

        public void CycleNextLaunchPoint() => CycleLaunchPoint(1);

        public void PrepareForLaunch()
        {
            m_ProjectileRigidbody.isKinematic = true;
            m_ProjectileRigidbody.transform.parent = transform;
            m_ProjectileRigidbody.transform.localPosition = Vector3.zero;
            m_ProjectileRigidbody.transform.localRotation = Quaternion.identity;
            ReadyForLaunch = true;
            LaunchPrepared?.Invoke();
        }

        public void Launch()
        {
            m_ProjectileRigidbody.transform.parent = null;
            m_ProjectileRigidbody.isKinematic = false;
            m_ProjectileRigidbody.AddForceAtPosition(LaunchForce * m_ProjectileRigidbody.mass, m_ForcePoint.transform.position, ForceMode.Impulse);
            ReadyForLaunch = false;
            Launched?.Invoke();
        }

        private void Turn(int direction)
        {
            transform.Rotate(direction * m_TurnSpeed * Time.deltaTime * Vector3.up, Space.World);
            IsTurning = true;
            if (m_StopTurningCoroutine != null)
            {
                StopCoroutine(m_StopTurningCoroutine);
            }
            m_StopTurningCoroutine = StartCoroutine(StopTurningRoutine());
        }

        private void ChangeLaunchPower(int direction)
        {
            m_LaunchPower += Time.deltaTime * m_LaunchPowerDelta * direction;
            m_LaunchPower = Mathf.Clamp(m_LaunchPower, m_MinLaunchPower, m_MaxLaunchPower);
            IsPowering = true;
            if (m_StopPoweringCoroutine != null)
            {
                StopCoroutine(m_StopPoweringCoroutine);
            }
            m_StopPoweringCoroutine = StartCoroutine(StopPoweringRoutine());
        }

        private void CycleLaunchPoint(int direction)
        {
            m_LaunchPower = m_MinLaunchPower;

            m_LaunchPointTracker.CycleLaunchPoint(direction);
            var launchPoint = m_LaunchPointTracker.LaunchPoint;

            transform.position = launchPoint.Position.WithY(launchPoint.Position.y + m_VerticalOffset);
            transform.rotation = Quaternion.Euler(m_LaunchAngle, launchPoint.TurnAngle, 0);
        }

        private IEnumerator StopTurningRoutine()
        {
            yield return null;
            yield return null;
            IsTurning = false;
        }

        private IEnumerator StopPoweringRoutine()
        {
            yield return null;
            yield return null;
            IsPowering = false;
        }
    }
}
