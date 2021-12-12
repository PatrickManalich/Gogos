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
        private TextMeshProUGUI m_PlayerNameText;

        [SerializeField]
        private ScriptableColorPalette m_ScriptableColorPalette;

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

        public void SetDetails(AbstractGogo gogo)
        {
            var scriptableGogo = gogo.IdentifiableGogo.ScriptableGogo;
            var player = gogo.Player;

            m_NicknameText.SetNickname(scriptableGogo.Nickname, scriptableGogo.RarityTier);
            m_GogoVariantIcon.SetIcon(scriptableGogo.GogoVariant);

            var palette = m_ScriptableColorPalette;
            var isUnclaimed = player == null;
            m_PlayerNameText.text = isUnclaimed ? "Unclaimed" : player.Name;
            m_PlayerNameText.color = isUnclaimed ? palette.Grey : palette.GetColorForPlayerColor(player.PlayerColor);

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
                    var blastForceTierTracker = (BlastForceTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.BlastForce);
                    m_BlastForceTierDetails.SetSlots(blastForceTierTracker.Tier, blastScriptableGogo.BlastForceTier);
                    m_BlastShapeIllustration.sprite = blastScriptableGogo.BlastShapeIllustration;
                    m_BlastDetails.SetActive(true);
                    break;

                case GogoVariant.Shield:
                    var shieldScriptableGogo = (ShieldScriptableGogo)scriptableGogo;
                    var shieldStrengthTierTracker = (ShieldStrengthTierTracker)gogo.TierTrackerReference.GetTierTrackerForVariant(TierVariant.ShieldStrength);
                    m_ShieldStrengthTierDetails.SetSlots(shieldStrengthTierTracker.Tier, shieldScriptableGogo.ShieldStrengthTier);
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
