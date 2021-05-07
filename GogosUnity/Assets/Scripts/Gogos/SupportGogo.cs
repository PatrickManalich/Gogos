using UnityEngine;

namespace Gogos
{
    public class SupportGogo : MonoBehaviour
    {
        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private Animator m_SupportTriggerAnimator;

        private const string ExpandName = "Expand";

        private void Start()
        {
            m_Accelerometer.StartedMoving += Accelerometer_OnStartedMoving;
            m_Accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;

            m_SupportTriggerAnimator.SetBool(ExpandName, true);
        }

        private void OnDestroy()
        {
            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
            m_Accelerometer.StartedMoving -= Accelerometer_OnStartedMoving;
        }

        private void Accelerometer_OnStartedMoving()
        {
            m_SupportTriggerAnimator.SetBool(ExpandName, false);
        }

        private void Accelerometer_OnStoppedMoving()
        {
            m_SupportTriggerAnimator.SetBool(ExpandName, true);
        }
    }
}