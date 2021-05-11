using UnityEngine;

namespace Gogos
{
    public class SupportGogo : MonoBehaviour
    {
        [SerializeField]
        private Accelerometer m_Accelerometer;

        [SerializeField]
        private TerrainSnapper m_TriggerRangeTerrainSnapper;

        [SerializeField]
        private SupportTrigger m_SupportTrigger;

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
            m_SupportTrigger.RemoveSupport();
        }

        private void Accelerometer_OnStoppedMoving()
        {
            m_TriggerRangeTerrainSnapper.SnapToTerrain();
            m_SupportTrigger.ProvideSupport();
        }
    }
}