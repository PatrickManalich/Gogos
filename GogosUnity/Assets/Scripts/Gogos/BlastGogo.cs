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
        private RotationAligner m_TriggerRangeRotationAligner;

        [SerializeField]
        private BlastTrigger m_BlastTrigger;

        private Quaternion m_GogoLauncherAlignedRotation; 

        private void Start()
        {
            var gogoLauncher = FindObjectOfType<GogoLauncher>();
            m_GogoLauncherAlignedRotation = Quaternion.Euler(new Vector3(0, gogoLauncher.transform.rotation.eulerAngles.y, 0));

            m_Accelerometer.StoppedMoving += Accelerometer_OnStoppedMoving;
        }

        private void OnDestroy()
        {
            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
        }

        private void Accelerometer_OnStoppedMoving()
        {
            m_TriggerRangeRefresher.gameObject.SetActive(false);
            m_TriggerRangeRotationAligner.AlignWithRotation(m_GogoLauncherAlignedRotation);
            m_BlastTrigger.Blast();

            m_Accelerometer.StoppedMoving -= Accelerometer_OnStoppedMoving;
        }
    }
}