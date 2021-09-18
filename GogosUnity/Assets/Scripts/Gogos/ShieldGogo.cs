using UnityEngine;

namespace Gogos
{
    public class ShieldGogo : AbstractGogo
    {
        [SerializeField]
        private GroundSnapper m_TriggerRangeGroundSnapper;

        [SerializeField]
        private ShieldTrigger m_ShieldTrigger;

        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        protected override void Start()
        {
            base.Start();
            m_ShieldStrengthTierTracker = (ShieldStrengthTierTracker)m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength);
        }

        public override void SetTiers(AbstractScriptableGogo scriptableGogo)
        {
            base.SetTiers(scriptableGogo);
            var shieldScriptableGogo = (ShieldScriptableGogo)scriptableGogo;
            m_TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength).SetTier((int)shieldScriptableGogo.ShieldStrengthTier);
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
                m_ShieldTrigger.EnableShield();
            }
        }
    }
}
