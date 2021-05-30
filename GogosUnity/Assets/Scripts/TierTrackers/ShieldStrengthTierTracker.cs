using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
	public enum ShieldStrengthTier { Broken, Weak, Medium, Strong }

	public class ShieldStrengthTierTracker : AbstractTierTracker<ShieldStrengthTier>
	{
        public override TierVariant TierVariant => TierVariant.ShieldStrength;
        public override ShieldStrengthTier CurrentTier { get => m_CurrentTier; protected set => m_CurrentTier = value; }

        public bool IsShieldBroken => CurrentTier == ShieldStrengthTier.Broken;
        public float ShieldAlpha => ShieldAlphaByTier[CurrentTier];

        private static readonly Dictionary<ShieldStrengthTier, float> ShieldAlphaByTier = new Dictionary<ShieldStrengthTier, float>()
        {
            { ShieldStrengthTier.Broken, 0 },
            { ShieldStrengthTier.Weak, 0.1f },
            { ShieldStrengthTier.Medium, 0.2f },
            { ShieldStrengthTier.Strong, 0.3f },
        };

        [SerializeField]
        private ShieldStrengthTier m_CurrentTier;
    }
}