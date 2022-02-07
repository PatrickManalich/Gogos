using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ShieldGogo", menuName = "Gogos/Scriptable Gogo/Shield")]
    public class ShieldScriptableGogo : AbstractScriptableGogo
    {
        public override GogoClass GogoClass => GogoClass.Shield;

        [SerializeField]
        private ShieldStrengthTier m_ShieldStrengthTier;
        public ShieldStrengthTier ShieldStrengthTier => m_ShieldStrengthTier;

        [SerializeField]
        private GroupsByShieldResponse m_GroupsByShieldResponse;
        public GroupsByShieldResponse ShieldResponsesByGroups => m_GroupsByShieldResponse;
    }
}
