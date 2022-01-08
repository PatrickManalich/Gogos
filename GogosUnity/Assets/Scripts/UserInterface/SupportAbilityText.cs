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

        public void SetText(Groups supportableGroups, TierVariant tierVariant, int tierModifier)
        {
            var supportableGroupNames = new List<string>();
            foreach (Groups group in Enum.GetValues(typeof(Groups)))
            {
                if (supportableGroups.HasFlag(group))
                {
                    supportableGroupNames.Add(group.ToString());
                }
            }
            var supportableGroupsText = string.Join("/", supportableGroupNames).SplitOnCamelCase();
            var tierVariantText = tierVariant.ToString().SplitOnCamelCase();
            var tierModifierText = tierModifier.ToString("+0;-#");
            m_Text.text = $"{supportableGroupsText} <u>{tierVariantText}</u> {tierModifierText}";
        }
    }
}
