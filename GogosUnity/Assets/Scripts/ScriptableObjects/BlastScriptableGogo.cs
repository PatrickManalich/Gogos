using UnityEngine;

namespace Gogos
{
    [CreateAssetMenu(fileName = "BlastGogo", menuName = "Gogos/Scriptable Gogo/Blast")]
    public class BlastScriptableGogo : AbstractScriptableGogo
    {
        public override GogoVariant GogoVariant => GogoVariant.Blast;

        [SerializeField]
        private BlastPowerTier m_BlastPowerTier;
        public BlastPowerTier BlastPowerTier => m_BlastPowerTier;

        [SerializeField]
        private Sprite[] m_BlastShapeSprites;
        public Sprite[] BlastShapeSprites => m_BlastShapeSprites;
    }
}
