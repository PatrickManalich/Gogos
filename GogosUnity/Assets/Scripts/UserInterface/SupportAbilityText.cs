using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gogos
{
    public class SupportAbilityText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        public void SetSupportAbility(SupportableGroups supportableGroups, TierVariant tierVariant, int tierModifier)
        {
            var supportableGroupNames = new List<string>();
            foreach (SupportableGroups supportableGroup in Enum.GetValues(typeof(SupportableGroups)))
            {
                if (supportableGroups.HasFlag(supportableGroup))
                {
                    supportableGroupNames.Add(supportableGroup.ToString());
                }
            }
            var supportableGroupsText = string.Join("/", supportableGroupNames).SplitOnCamelCase();
            var tierVariantText = tierVariant.ToString().SplitOnCamelCase();
            var tierModifierText = tierModifier.ToString("+0;-#");
            m_Text.text = $"{supportableGroupsText} {tierVariantText} {tierModifierText}";
        }
    }
}
