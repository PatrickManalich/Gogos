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
        public Vector3 LaunchForce => transform.forward * m_CurrentLaunchPower;
        public GameObject Projectile => m_ProjectileRigidbody?.gameObject;
        public bool ReadyForLaunch { get; private set; }

        [SerializeField]
        private GameObject m_ForcePoint;

        [SerializeField]
        private GameObject m_EnvironmentCenter;

        [SerializeField]
        private float m_LaunchAngle;

        [SerializeField]
        private float m_MinLaunchPower;

        [SerializeField]
        private float m_MaxLaunchPower;

        [SerializeField]
        private float m_LaunchPowerDelta;

        [SerializeField]
        private float m_MovementSpeed;

        private float m_CurrentLaunchPower;
        private float m_DistanceToTarget;
        private float m_MovementAngle;
        private Rigidbody m_ProjectileRigidbody;

        private void Start()
        {
            m_DistanceToTarget = Vector3.Distance(transform.position, m_EnvironmentCenter.transform.position);
            m_MovementAngle = 270;
            Align();
        }

        private void OnEnable()
        {
            m_CurrentLaunchPower = (m_MinLaunchPower + m_MaxLaunchPower) / 2;
        }

        public void LoadProjectile(GameObject projectile)
        {
            m_ProjectileRigidbody = projectile.GetComponent<Rigidbody>();
            PrepareForLaunch();
        }

        public void MoveLeft() => Move(-1);

        public void MoveRight() => Move(1);

        public void DecreaseLaunchPower() => ChangeLaunchPower(-1);

        public void IncreaseLaunchPower() => ChangeLaunchPower(1);

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

        private void Move(int direction)
        {
            m_MovementAngle += Time.deltaTime * m_MovementSpeed * direction;
            m_MovementAngle = m_MovementAngle.ClampAngle();
            Align();
        }

        private void ChangeLaunchPower(int direction)
        {
            m_CurrentLaunchPower += Time.deltaTime * m_LaunchPowerDelta * direction;
            m_CurrentLaunchPower = Mathf.Clamp(m_CurrentLaunchPower, m_MinLaunchPower, m_MaxLaunchPower);
        }

        private void Align()
        {
            var x = Mathf.Cos(m_MovementAngle.DegreesToRadians()) * m_DistanceToTarget;
            var y = 0;
            var z = Mathf.Sin(m_MovementAngle.DegreesToRadians()) * m_DistanceToTarget;
            transform.position = new Vector3(x, y, z);
            transform.LookAt(m_EnvironmentCenter.transform);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithX(m_LaunchAngle));
        }
    }
}
