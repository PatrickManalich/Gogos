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
        private TierDetails m_WeightTierDetails;

        [SerializeField]
        private TierDetails m_RangeTierDetails;

        [Header("Blast")]
        [SerializeField]
        private GameObject m_BlastDetails;

        [SerializeField]
        private TierDetails m_BlastForceTierDetails;

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
        private TextMeshProUGUI m_SupportAbilityText;

        public void SetDetails(AbstractScriptableGogo scriptableGogo)
        {
            m_NicknameText.SetNickname(scriptableGogo.Nickname, scriptableGogo.RarityTier);
            m_GogoVariantIcon.SetIcon(scriptableGogo.GogoVariant);
            m_WeightTierDetails.SetSlots(scriptableGogo.WeightTier);
            m_RangeTierDetails.SetSlots(scriptableGogo.RangeTier);

            m_BlastDetails.SetActive(false);
            m_ShieldDetails.SetActive(false);
            m_SupportDetails.SetActive(false);

            switch (scriptableGogo.GogoVariant)
            {
                case GogoVariant.Blast:
                    var blastScriptableGogo = (BlastScriptableGogo)scriptableGogo;
                    m_BlastForceTierDetails.SetSlots(blastScriptableGogo.BlastForceTier);
                    m_BlastShapeIllustration.sprite = blastScriptableGogo.BlastShapeIllustration;
                    m_BlastDetails.SetActive(true);
                    break;

                case GogoVariant.Shield:
                    var shieldScriptableGogo = (ShieldScriptableGogo)scriptableGogo;
                    m_ShieldStrengthTierDetails.SetSlots(shieldScriptableGogo.ShieldStrengthTier);
                    m_ShieldDetails.SetActive(true);
                    break;

                case GogoVariant.Support:
                    var supportScriptableGogo = (SupportScriptableGogo)scriptableGogo;
                    var tierVariantText = supportScriptableGogo.SupportAbilityTierVariant.ToString();
                    var tierModifierText = supportScriptableGogo.SupportAbilityTierModifier.ToString("+0;-#");
                    m_SupportAbilityText.text = $"Ally Gogo {tierVariantText} {tierModifierText}";
                    m_SupportDetails.SetActive(true);
                    break;
            }
        }
    }
}
