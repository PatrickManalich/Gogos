using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "ShieldGogo", menuName = "Gogos/Scriptable Gogo/Shield")]
    public class ShieldScriptableGogo : AbstractScriptableGogo
    {
        public override GogoVariant GogoVariant => GogoVariant.Shield;

        [SerializeField]
        private ShieldStrengthTier m_ShieldStrengthTier;
        public ShieldStrengthTier ShieldStrengthTier => m_ShieldStrengthTier;
    }
}