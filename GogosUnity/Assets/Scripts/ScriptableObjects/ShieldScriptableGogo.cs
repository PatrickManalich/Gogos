using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ShieldGogo", menuName = "Gogos/Scriptable Gogo/Shield")]
    public class ShieldScriptableGogo : AbstractScriptableGogo
    {
        public override GogoClass GogoClass => GogoClass.Shield;

        [SerializeField]
        private ShieldDurabilityTier m_ShieldDurabilityTier;
        public ShieldDurabilityTier ShieldDurabilityTier => m_ShieldDurabilityTier;

        [SerializeField]
        private GroupsByShieldResponse m_GroupsByShieldResponse;
        public GroupsByShieldResponse ShieldResponsesByGroups => m_GroupsByShieldResponse;
    }
}
