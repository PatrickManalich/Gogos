using UnityEngine;

namespace Gogos
{
    public class SupportGogo : AbstractGogo
    {
        [SerializeField]
        private SupportAbility m_SupportAbility;

        [SerializeField]
        private GroundSnapper m_TriggerRangeGroundSnapper;

        [SerializeField]
        private SupportTrigger m_SupportTrigger;

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            m_SupportAbility.SetPlayer(Player);
            m_SupportTrigger.SetPlayer(Player);
        }

        public override void SetTiers(IdentifiableGogo identifiableGogo)
        {
            base.SetTiers(identifiableGogo);
            var supportScriptableGogo = (SupportScriptableGogo)IdentifiableGogo.ScriptableGogo;
            m_SupportAbility.SetAbility(supportScriptableGogo.Groups, supportScriptableGogo.SupportAbilityTierVariant, supportScriptableGogo.SupportAbilityTierModifier);
        }

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
