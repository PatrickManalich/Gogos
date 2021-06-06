using UnityEngine;

namespace Gogos
{
    public class ShieldGogo : AbstractGogo
    {
        [SerializeField]
        private ShieldStrengthTierTracker m_ShieldStrengthTierTracker;

        [SerializeField]
        private ShieldTrigger m_ShieldTrigger;

        protected override void SetTiers(ScriptableGogo scriptableGogo)
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
                m_ShieldTrigger.EnableShield();
            }
        }
    }
}