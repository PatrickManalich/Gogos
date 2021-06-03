﻿using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "SupportGogo", menuName = "Gogos/Scriptable Gogo/Support")]
    public class SupportScriptableGogo : ScriptableGogo
    {
        [SerializeField]
        private TierVariant m_SupportAbilityTierVariant;
        public TierVariant SupportAbilityTierVariant => m_SupportAbilityTierVariant;

        [Range(SupportAbility.MinSupport, SupportAbility.MaxSupport)]
        [SerializeField]
        private int m_SupportAbilityTierModifier;
        public int SupportAbilityTierModifier => m_SupportAbilityTierModifier;
    }
}