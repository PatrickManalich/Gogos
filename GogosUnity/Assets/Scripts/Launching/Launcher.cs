using Gogos.Extensions;
using System;
using UnityEngine;

namespace Gogos
{
    public class Launcher : MonoBehaviour
    {
        public event Action LaunchPrepared;

        public event Action Launched;

        public Vector3 LaunchPoint => transform.position;
        public Vector3 LaunchForce => transform.forward * m_LaunchPower;
        public GameObject Projectile => m_ProjectileRigidbody?.gameObject;
        public bool ReadyForLaunch { get; private set; }

        [SerializeField]
        private GameObject m_ForcePoint;

        [SerializeField]
        private GameObject m_EnvironmentCenter;

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
        private int m_LaunchPointIndex;
        private Rigidbody m_ProjectileRigidbody;

        private void OnEnable()
        {
            m_LaunchPower = (m_MinLaunchPower + m_MaxLaunchPower) / 2;

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
        }

        private void ChangeLaunchPower(int direction)
        {
            m_LaunchPower += Time.deltaTime * m_LaunchPowerDelta * direction;
            m_LaunchPower = Mathf.Clamp(m_LaunchPower, m_MinLaunchPower, m_MaxLaunchPower);
        }

        private void CycleLaunchPoint(int direction)
        {
            m_LaunchPointIndex += direction;
            if (m_LaunchPointIndex > m_LaunchPointTracker.LaunchPoints.Count - 1)
            {
                m_LaunchPointIndex = 0;
            }
            else if (m_LaunchPointIndex < 0)
            {
                m_LaunchPointIndex = m_LaunchPointTracker.LaunchPoints.Count - 1;
            }

            transform.position = m_LaunchPointTracker.LaunchPoints[m_LaunchPointIndex];
            transform.position = transform.position.WithY(transform.position.y + m_VerticalOffset);
            transform.LookAt(m_EnvironmentCenter.transform);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithX(m_LaunchAngle));
        }
    }
}
