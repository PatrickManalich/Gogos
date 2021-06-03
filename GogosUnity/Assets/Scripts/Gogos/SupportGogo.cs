using UnityEngine;

namespace Gogos
{
    public class SupportGogo : AbstractGogo
    {
        [SerializeField]
        private TerrainSnapper m_TriggerRangeTerrainSnapper;

        [SerializeField]
        private SupportTrigger m_SupportTrigger;

        protected override void OnStartedMoving()
        {
            m_SupportTrigger.RemoveSupport();
        }

        protected override void OnStoppedMoving()
        {
            m_TriggerRangeTerrainSnapper.SnapToTerrain();
            m_SupportTrigger.ProvideSupport();
        }
    }
}