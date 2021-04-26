﻿using System.Collections.Generic;
using UnityEngine;

namespace Gogos
{
    public enum BlastForceTier { Weak, Medium, Strong }

    public class BlastForceTierTracker: AbstractTierTracker<BlastForceTier>
	{
        public override TierVariant TierVariant => TierVariant.BlastForce;
        public override BlastForceTier CurrentTier { get => m_CurrentTier; protected set => m_CurrentTier = value; }
       
        public float BlastForce => BlastForceByTier[CurrentTier];
        public float BlastUpwardsModifier => BlastUpwardsModifierByTier[CurrentTier];
        
        private static readonly Dictionary<BlastForceTier, float> BlastForceByTier = new Dictionary<BlastForceTier, float>()
        {
            { BlastForceTier.Weak, 7 },
            { BlastForceTier.Medium, 9 },
            { BlastForceTier.Strong, 11 },
        };
        private static readonly Dictionary<BlastForceTier, float> BlastUpwardsModifierByTier = new Dictionary<BlastForceTier, float>()
        {
            { BlastForceTier.Weak, 2 },
            { BlastForceTier.Medium, 2 },
            { BlastForceTier.Strong, 2 },
        };

        [SerializeField]
        private BlastForceTier m_CurrentTier;
    }
}