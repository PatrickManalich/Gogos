// Copyright 2020-2021 Barron Associates, Inc.
// Proprietary Information - All Rights Reserved
using UnityEngine;

namespace Gogos
{
    public class GogoDetailsPanel : MonoBehaviour
    {
        [SerializeField]
        private NicknameText m_NicknameText;

        [SerializeField]
        private PlayerNameText m_PlayerNameText;

        [SerializeField]
        private TierDetails m_WeightTierDetails;

        [SerializeField]
        private TierDetails m_RangeTierDetails;

        [Header("Blast")]
        [SerializeField]
        private GameObject m_BlastDetails;

        [SerializeField]
        private TierDetails m_BlastPowerTierDetails;

        [SerializeField]
        private BlastShapeIllustration m_BlastShapeIllustration;

        [Header("Shield")]
        [SerializeField]
        private GameObject m_ShieldDetails;

        [SerializeField]
        private TierDetails m_ShieldStrengthTierDetails;

        [SerializeField]
        private ShieldAbilityText m_ShieldAbilityText;

        [Header("Support")]
        [SerializeField]
        private GameObject m_SupportDetails;

        [SerializeField]
        private SupportAbilityText m_SupportAbilityText;

        public void SetDetails(AbstractGogo gogo)
        {
            var scriptableGogo = gogo.IdentifiableGogo.ScriptableGogo;

            m_NicknameText.SetText(scriptableGogo.Nickname, scriptableGogo.RarityTier);
            m_PlayerNameText.SetText(gogo.Player);
            var weightTierTracker = (WeightTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.Weight);
            m_WeightTierDetails.SetSlots(weightTierTracker.Tier, scriptableGogo.WeightTier);
            var rangeTierTracker = (RangeTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.Range);
            m_RangeTierDetails.SetSlots(rangeTierTracker.Tier, scriptableGogo.RangeTier);

            m_BlastDetails.SetActive(false);
            m_ShieldDetails.SetActive(false);
            m_SupportDetails.SetActive(false);

            switch (scriptableGogo.GogoVariant)
            {
                case GogoVariant.Blast:
                    m_BlastDetails.SetActive(true);
                    var blastScriptableGogo = (BlastScriptableGogo)scriptableGogo;
                    var blastPowerTierTracker = (BlastPowerTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastPower);
                    m_BlastPowerTierDetails.SetSlots(blastPowerTierTracker.Tier, blastScriptableGogo.BlastPowerTier);
                    m_BlastShapeIllustration.SetIllustration(blastScriptableGogo.BlastShapeSprites);
                    break;

                case GogoVariant.Shield:
                    // Account for Broken tier
                    m_ShieldDetails.SetActive(true);
                    var shieldScriptableGogo = (ShieldScriptableGogo)scriptableGogo;
                    var shieldStrengthTierTracker = (ShieldStrengthTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength);
                    m_ShieldStrengthTierDetails.SetSlots(shieldStrengthTierTracker.Tier - 1, shieldScriptableGogo.ShieldStrengthTier - 1);
                    m_ShieldAbilityText.SetText(shieldScriptableGogo.ShieldResponsesByGroups);
                    break;

                case GogoVariant.Support:
                    m_SupportDetails.SetActive(true);
                    var supportScriptableGogo = (SupportScriptableGogo)scriptableGogo;
                    m_SupportAbilityText.SetText(supportScriptableGogo.Groups, supportScriptableGogo.SupportAbilityTierVariant, supportScriptableGogo.SupportAbilityTierModifier);
                    break;
            }
        }
    }
}
