using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "SupportGogo", menuName = "Gogos/Scriptable Gogo/Support")]
    public class SupportScriptableGogo : AbstractScriptableGogo
    {
        public override GogoClass GogoClass => GogoClass.Support;

        [SerializeField]
        private Groups m_Groups;
        public Groups Groups => m_Groups;

        [SerializeField]
        private TierVariant m_SupportAbilityTierVariant;
        public TierVariant SupportAbilityTierVariant => m_SupportAbilityTierVariant;

        [Range(SupportAbility.MinSupport, SupportAbility.MaxSupport)]
        [SerializeField]
        private int m_SupportAbilityTierModifier;
        public int SupportAbilityTierModifier => m_SupportAbilityTierModifier;
    }
}
