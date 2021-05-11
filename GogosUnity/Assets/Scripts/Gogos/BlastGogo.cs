using UnityEngine;

namespace Gogos
{
    public class BlastGogo : MonoBehaviour
    {
        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private TriggerRangeRefresher m_TriggerRangeRefresher;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

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
            m_TriggerRangeRefresher.gameObject.SetActive(false);
            m_BlastTrigger.Blast();

            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
        }
    }
}