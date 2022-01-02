using UnityEngine;

namespace Gogos
{
    public class ShieldGogo : AbstractGogo
    {
        [SerializeField]
        private ShieldAbility m_ShieldAbility;

        [SerializeField]
        private GroundSnapper m_TriggerRangeGroundSnapper;

        [SerializeField]
        private ShieldTrigger m_ShieldTrigger;

        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        protected override void Start()
        {
            base.Start();
            m_ShieldStrengthTierTracker = (ShieldStrengthTierTracker)TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength);
        }

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            m_ShieldTrigger.Player = Player;
        }

        public override void SetTiers(IdentifiableGogo identifiableGogo)
        {
            base.SetTiers(identifiableGogo);
            var shieldScriptableGogo = (ShieldScriptableGogo)IdentifiableGogo.ScriptableGogo;
            TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength).SetTier((int)shieldScriptableGogo.ShieldStrengthTier);
            m_ShieldAbility.SetAbility(shieldScriptableGogo.ShieldResponsesByGroups);
        }

        protected override void OnStartedMoving()
        {
            m_ShieldTrigger.DisableShield();
        }

        protected override void OnStoppedMoving()
        {
            if (!m_ShieldStrengthTierTracker.IsShieldBroken)
            {
                m_TriggerRangeGroundSnapper.SnapToGround();
                m_ShieldTrigger.EnableShield(m_ShieldStrengthTierTracker, m_ShieldAbility);
            }
        }
    }
}
