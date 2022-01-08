using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gogos
{
    public class ShieldAbilityText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_Text;

        public void SetText(GroupsByShieldResponse groupsByShieldResponse)
        {
            var shieldResponses = groupsByShieldResponse.Keys;
            var shieldResponseLines = new List<string>();
            foreach (var shieldResponse in shieldResponses)
            {
                var shieldableGroup = groupsByShieldResponse[shieldResponse];
                var shieldableGroupNames = new List<string>();
                foreach (Groups group in Enum.GetValues(typeof(Groups)))
                {
                    if (shieldableGroup.HasFlag(group))
                    {
                        shieldableGroupNames.Add(group.ToString());
                    }
                }
                var shieldResponseLine = $"<u>{shieldResponse}</u>: { string.Join("/", shieldableGroupNames).SplitOnCamelCase()}";
                shieldResponseLines.Add(shieldResponseLine);
            }
            m_Text.text = string.Join(Environment.NewLine, shieldResponseLines);
        }
    }
}
