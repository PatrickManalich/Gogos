using UnityEngine;

namespace Gogos
{
    public class ShieldGogo : MonoBehaviour
    {
        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        [SerializeField]
        private ShieldTrigger m_ShieldTrigger;

        private void Start()
        {
            m_Accelerometer.StartedMoving += Accelerometer_OnStartedMoving;
            m_Accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
        }

        private void OnDestroy()
        {
            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
            m_Accelerometer.StartedMoving -= Accelerometer_OnStartedMoving;
        }

        private void Accelerometer_OnStartedMoving()
        {
            m_ShieldTrigger.DisableShield();
        }

        private void Accelerometer_OnStoppedMoving()
        {
            if (!m_ShieldStrengthTierTracker.IsShieldBroken)
            {
                m_ShieldTrigger.EnableShield();
            }
        }
    }
}