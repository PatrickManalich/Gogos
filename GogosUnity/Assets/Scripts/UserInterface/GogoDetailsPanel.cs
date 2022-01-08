// Copyright 2020-2021 Barron Associates, Inc.
// Proprietary Information - All Rights Reserved
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gogos
{
    public class GogoDetailsPanel : MonoBehaviour
    {
        [SerializeField]
        private NicknameText m_NicknameText;

        [SerializeField]
        private GogoVariantIcon m_GogoVariantIcon;

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
        private Image m_BlastShapeIllustration;

        [Header("Shield")]
        [SerializeField]
        private GameObject m_ShieldDetails;

        [SerializeField]
        private TierDetails m_ShieldStrengthTierDetails;

        [Header("Support")]
        [SerializeField]
        private GameObject m_SupportDetails;

        [SerializeField]
        private SupportAbilityText m_SupportAbilityText;

        public void SetDetails(AbstractGogo gogo)
        {
            var scriptableGogo = gogo.IdentifiableGogo.ScriptableGogo;

            m_NicknameText.SetText(scriptableGogo.Nickname, scriptableGogo.RarityTier);
            m_GogoVariantIcon.SetIcon(scriptableGogo.GogoVariant);
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
                    var blastScriptableGogo = (BlastScriptableGogo)scriptableGogo;
                    var blastPowerTierTracker = (BlastPowerTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastPower);
                    m_BlastPowerTierDetails.SetSlots(blastPowerTierTracker.Tier, blastScriptableGogo.BlastPowerTier);
                    m_BlastShapeIllustration.sprite = blastScriptableGogo.BlastShapeIllustration;
                    m_BlastDetails.SetActive(true);
                    break;

                case GogoVariant.Shield:
                    // Account for Broken tier
                    var shieldScriptableGogo = (ShieldScriptableGogo)scriptableGogo;
                    var shieldStrengthTierTracker = (ShieldStrengthTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength);
                    m_ShieldStrengthTierDetails.SetSlots(shieldStrengthTierTracker.Tier - 1, shieldScriptableGogo.ShieldStrengthTier - 1);
                    m_ShieldDetails.SetActive(true);
                    break;

                case GogoVariant.Support:
                    var supportScriptableGogo = (SupportScriptableGogo)scriptableGogo;
                    m_SupportAbilityText.SetText(supportScriptableGogo.Groups, supportScriptableGogo.SupportAbilityTierVariant, supportScriptableGogo.SupportAbilityTierModifier);
                    m_SupportDetails.SetActive(true);
                    break;
            }
        }
    }
}
