using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "SupportGogo", menuName = "Gogos/Scriptable Gogo/Support")]
    public class SupportScriptableGogo : AbstractScriptableGogo
    {
        public override GogoVariant GogoVariant => GogoVariant.Support;

        [SerializeField]
        private SupportableGroups m_SupportableGroups;
        public SupportableGroups SupportableGroups => m_SupportableGroups;

        [SerializeField]
        private TierVariant m_SupportAbilityTierVariant;
        public TierVariant SupportAbilityTierVariant => m_SupportAbilityTierVariant;

        [Range(SupportAbility.MinSupport, SupportAbility.MaxSupport)]
        [SerializeField]
        private int m_SupportAbilityTierModifier;
        public int SupportAbilityTierModifier => m_SupportAbilityTierModifier;
    }
}
