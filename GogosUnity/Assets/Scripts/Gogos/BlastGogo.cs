using UnityEngine;

namespace Gogos
{
    public class BlastGogo : MonoBehaviour
    {
        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private Animator m_BlastTriggerAnimator;

        private const string ExpandName = "Expand";

        private void Start()
        {
            m_Accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
        }

        private void OnDestroy()
        {
            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
        }

        private void Accelerometer_OnStoppedMoving()
        {
            m_BlastTriggerAnimator.SetTrigger(ExpandName);

            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
        }
    }
}