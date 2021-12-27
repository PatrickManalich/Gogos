using TMPro;
using UnityEngine;

namespace Gogos
{
    public class SupportAbilityText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        public void SetSupportAbility(TierVariant tierVariant, int tierModifier)
        {
            var tierVariantText = tierVariant.ToString().SplitOnCamelCase();
            var tierModifierText = tierModifier.ToString("+0;-#");
            m_Text.text = $"Ally Gogo {tierVariantText} {tierModifierText}";
        }
    }
}
