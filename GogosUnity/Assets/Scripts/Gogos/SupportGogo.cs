using UnityEngine;

namespace Gogos
{
    public class SupportGogo : AbstractGogo
    {
        [SerializeField]
        private SupportAbility m_SupportAbility;

        [SerializeField]
        private TerrainSnapper m_TriggerRangeTerrainSnapper;

        [SerializeField]
        private SupportTrigger m_SupportTrigger;

        protected override void SetTiers(AbstractScriptableGogo scriptableGogo)
        {
            base.SetTiers(scriptableGogo);
            var supportScriptableGogo = (SupportScriptableGogo)scriptableGogo;
            m_SupportAbility.SetAbility(supportScriptableGogo.SupportAbilityTierVariant, supportScriptableGogo.SupportAbilityTierModifier);
        }

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