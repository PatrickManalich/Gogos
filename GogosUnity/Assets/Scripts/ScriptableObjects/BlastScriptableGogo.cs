using UnityEngine;

namespace Gogos
{
	[CreateAssetMenu(fileName = "BlastGogo", menuName = "Gogos/Scriptable Gogo/Blast")]
	public class BlastScriptableGogo : AbstractScriptableGogo
	{
        public override GogoVariant GogoVariant => GogoVariant.Blast;

        [SerializeField]
        private BlastForceTier m_BlastForceTier;
        public BlastForceTier BlastForceTier => m_BlastForceTier;

        [SerializeField]
        private Sprite m_BlastShapeIllustration;
        public Sprite BlastShapeImage => m_BlastShapeIllustration;
    }
}