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
            m_SupportTrigger.Player = Player;
        }

        public override void SetTiers(IdentifiableGogo identifiableGogo)
        {
            base.SetTiers(identifiableGogo);
            var supportScriptableGogo = (SupportScriptableGogo)IdentifiableGogo.ScriptableGogo;
            m_SupportAbility.SetAbility(supportScriptableGogo.SupportableGroups, supportScriptableGogo.SupportAbilityTierVariant, supportScriptableGogo.SupportAbilityTierModifier);
        }

        protected override void OnStartedMoving()
        {
            m_SupportTrigger.RemoveSupport();
        }

        protected override void OnStoppedMoving()
        {
            m_TriggerRangeGroundSnapper.SnapToGround();
            m_SupportTrigger.ProvideSupport();
        }
    }
}
