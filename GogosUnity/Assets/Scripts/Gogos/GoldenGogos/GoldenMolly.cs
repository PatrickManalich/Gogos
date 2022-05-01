using UnityEngine;

namespace Gogos
{
    public class GoldenMolly : AbstractGogo
    {
        [SerializeField]
        private SupportAbility m_SupportAbility;

        [SerializeField]
        private GroundSnapper m_TriggerRangeGroundSnapper;

        [SerializeField]
        private SupportTrigger m_SupportTrigger;

        protected override void OnStartedMoving()
        {
            ReparentTriggerRange();
            m_SupportTrigger.RemoveSupport();
        }

        protected override void OnStoppedMoving()
        {
            m_TriggerRangeGroundSnapper.SnapToGround();
            UnparentTriggerRange();
            m_SupportTrigger.ProvideSupport(m_SupportAbility);
        }
    }
}
